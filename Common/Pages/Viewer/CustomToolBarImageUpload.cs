#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemos.Pages.Viewer
{
    public partial class CustomToolBarBase
    {
        //Class for the Custom stamp images
        internal class ImageDetails
        {
            public string? ImageData { get; set; }
            public bool ImageSelected { get; set; }
        }
        //Method for close the dialog box
        internal void ClosePopUp()
        {
            IsVisibleDialog = false;
            StateHasChanged();
        }
        //Method for selecting the custom imgage
        internal void ImageSelected(int i)
        {
            if (Images != null)
            {
                CustomStampSource = Images[i].ImageData!;
                RefreshImages();
                Images[i].ImageSelected = true;
            }
        }
        //Method for cancel button
        internal async Task CancelImage()
        {
            ClosePopUp();
            await ResetMode();
        }
        //Method for Add the custom stamp
        internal async Task AddImage()
        {
            foreach (ImageDetails image in Images)
            {
                if (image.ImageSelected)
                {
                    CustomStampSource = image.ImageData!;
                    await AddStamp();
                    break;
                }
            }
            ClosePopUp();
        }
        //Method for unselect the images
        internal void RefreshImages()
        {
            if (Images != null)
            {
                foreach (ImageDetails image in Images)
                {
                    image.ImageSelected = false;
                }
            }

        }

        //Triggers when changes occur in uploaded file list by selecting or dropping files.
        public async Task FileUploadSelected(UploadingEventArgs args)
        {
            if (args.FileData.Type == "pdf")
            {
                string base64string = args.FileData.RawFile.ToString()!;
                //Loads the PDF docuent from the given base64 string in the SfPdfViewer.
                await ViewerInstance!.LoadAsync(base64string, null!);
                await uploadFiles!.ClearAllAsync();
                if (module != null)
                {
                    await module.InvokeVoidAsync("changeFocus");
                }
            }
        }

        //Method for load the custom image
        internal async Task ImageChane(UploadChangeEventArgs action)
        {
            IsImageSelected = false;
            string base64 = action.Files[0].File.ToString()!;
            string type = action.Files[0].FileInfo.Type.ToLower();
            MemoryStream fileStream = new MemoryStream();
            await action.Files[0].File.OpenReadStream(long.MaxValue).CopyToAsync(fileStream);
            string baseString = Convert.ToBase64String(fileStream.ToArray());
            string image = "data:image/jpeg;base64," + baseString;
            RefreshImages();
            if (Images.Count > 2)
            {
                Images.Remove(Images[0]);
            }
            ImageDetails newImage = new ImageDetails()
            {
                ImageData = image,
                ImageSelected = true
            };
            Images.Add(newImage);
            CustomStampSource = Images[Images.Count - 1].ImageData!;
            ClosePopUp();
            await AddStamp();
            fileStream.Close();
        }
    }
}
