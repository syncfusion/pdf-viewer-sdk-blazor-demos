#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.SfPdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemos.Pages.Viewer
{
    public partial class ProgrammaticalAnnotationBase
    {
        //Method for Adding the Annotation in the pdf viewer
        internal async void AddAnnotation()
        {
            if (PdfViewerInstance != null)
            {
                PdfAnnotation newShapeAnnotation = new PdfAnnotation();
                AnnotationsPropertyUpdate(newShapeAnnotation);
                if (selectedAnnotation.AnnotationType != "Stamp")
                {
                    //Adding the Annotation in the PDF viewer
                    await PdfViewerInstance.AddAnnotationAsync(newShapeAnnotation);
                }
                else if (selectedAnnotation.StampsType == "Dynamic")
                {
                    DynamicStampItem dynamicAnnotationType = Enum.Parse<DynamicStampItem>(selectedAnnotation.DynamicStamp.Replace(" ", ""));
                    //Adding the Dynamic Stamp Annotation in the PDF viewer
                    await PdfViewerInstance.AddAnnotationAsync(newShapeAnnotation, dynamicAnnotationType);
                }
                else if (selectedAnnotation.StampsType == "SignHere")
                {
                    SignStampItem signAnnotationType = Enum.Parse<SignStampItem>(selectedAnnotation.SignHereStamp.Replace(" ", ""));
                    //Adding the Sign Stamp Annotation in the PDF viewer
                    await PdfViewerInstance.AddAnnotationAsync(newShapeAnnotation, signAnnotationType);
                }
                else if (selectedAnnotation.StampsType == "StandardBusiness")
                {
                    StandardBusinessStampItem standardBusinessAnnotationType = Enum.Parse<StandardBusinessStampItem>(selectedAnnotation.StandardBusinessStamp.Replace(" ", ""));
                    //Adding the Standard Business Stamp Annotation in the PDF viewer
                    await PdfViewerInstance.AddAnnotationAsync(newShapeAnnotation, standardBusinessAnnotationType);
                }
                //ResetAnnotationProperties(selectedAnnotation);
            }
        }
        //Method for reset the properties of the annotations
        internal void Reset()
        {
            ResetAnnotationProperties(selectedAnnotation);
        }
        //Method for Update the annotation in the pdf viewer
        internal async void UpdateAnnotation()
        {
            List<PdfAnnotation> pdfAnnotationList = new List<PdfAnnotation>();
            PdfAnnotation existingAnnotation = new PdfAnnotation();
            selectedAnnotation.IsInputChange = false;
            pdfAnnotationList = await PdfViewerInstance!.GetAnnotationsAsync();
            int existingAnnotationIndex = pdfAnnotationList.FindIndex(item => item.Id == selectedAnnotation.AnnotationId);
            if (existingAnnotationIndex != -1)
            {
                existingAnnotation = pdfAnnotationList[existingAnnotationIndex];

                AnnotationsPropertyUpdate(existingAnnotation);

                //Edit the Selected Annotation in the PDF viewer
                await PdfViewerInstance.EditAnnotationAsync(existingAnnotation);
            }
        }

        //Method for Update the annotation property values to the property panel when select the Annotation
        internal async Task AnnotationSelectedEvent(AnnotationSelectEventArgs annotationSelectEventArgs)
        {
            selectedAnnotation.AnnotationSelected = true;
            selectedAnnotation.AnnotationUnSelected = false;
            selectedAnnotation.IsInputChange = false;
            await UpdatePropertiesInPropertyPanel(annotationSelectEventArgs.AnnotationId);
        }
        //Method for Reset the annotation property values to the property panel when Add the Annotation
        internal void AddAnnotationEvent(AnnotationAddEventArgs annotationAddEventArgs)
        {
            //ResetAnnotationProperties(selectedAnnotation);
        }
        //Method for Update the annotation property values to the property panel when Move the Annotation
        internal async Task AnnotationMoved(AnnotationMoveEventArgs annotationSelectEventArgs)
        {
            selectedAnnotation.AnnotationSelected = true;
            selectedAnnotation.AnnotationUnSelected = false;
            await UpdatePropertiesInPropertyPanel(annotationSelectEventArgs.AnnotationId);
        }
        //Method for Reset the annotation property values to the property panel when Unselect the Annotation
        internal void AnnotationUnSelectedEvent()
        {
            selectedAnnotation.AnnotationUnSelected = true;
            selectedAnnotation.AnnotationSelected = false;
            ResetAnnotationProperties(selectedAnnotation);
        }
        //Method for Reset the annotation property values to the property panel when Remove the Annotation
        internal async Task AnnotationRemovedEvent(AnnotationRemoveEventArgs annotationSelectEventArgs)
        {
            selectedAnnotation.AnnotationUnSelected = true;
            selectedAnnotation.AnnotationSelected = false;
            ResetAnnotationProperties(selectedAnnotation);

        }

        //Method for the Find out the Total page number in the PDF file
        internal void DocumentLoaded()
        {
            selectedAnnotation.AnnotationType = "Rectangle";
            AnnotationUnSelectedEvent();
            if (PdfViewerInstance != null && numericTextBoxRef != null)
            {
                selectedAnnotation.TotalPageNumber = PdfViewerInstance.PageCount;
                numericTextBoxRef.Max = PdfViewerInstance.PageCount;
                StateHasChanged();
            }
        }

        //Method for the Ink annotation dropdown
        internal void InkAnnotationChanged(ChangeEventArgs<string, AnnotationsFields> args)
        {
            if (args.Value.ToString() == "PdfViewer")
            {
                selectedAnnotation.Data = "M462,144 L462,144 L463,144 L464,144 L465,147 L465,149 L465,150 L466,152 L466,154 L466,156 L466,156 L466,159 L466,160 L466,162 L466,163 L466,164 L466,165 L466,168 L466,169 L466,171 L466,172 L466,172 L466,174 L466,175 L466,176 L466,176 L466,178 L466,180 L467,180 L468,180 L468,181 M465,144 L465,144 L466,144 L468,144 L469,144 L470,144 L472,146 L473,147 L473,147 L474,148 L475,148 L475,149 L476,150 L476,151 L476,152 L476,152 L476,154 L476,155 L476,156 L476,156 L476,157 L476,158 L476,159 L476,160 L476,160 L475,160 L475,161 L474,162 L473,163 L472,164 L471,164 L469,165 L469,165 L468,166 L465,167 M479,148 L479,148 L479,149 L479,150 L479,152 L479,154 L477,156 L477,157 L477,158 L477,160 L477,161 L477,162 L477,163 L477,164 L477,166 L477,167 L477,168 L477,169 L477,170 L477,171 L477,172 L477,172 L477,173 L477,175 L477,176 L477,176 L477,178 L477,179 L477,180 M477,146 L477,146 L478,146 L480,146 L481,146 L484,146 L485,146 L485,146 L488,147 L489,148 L490,148 L490,148 L491,149 L493,149 L493,150 L493,151 L493,153 L493,154 L493,156 L494,158 L494,160 L494,161 L494,162 L494,163 L494,164 L494,165 L494,166 L494,168 L493,168 L492,169 L491,171 L490,172 L489,172 L488,174 L487,174 L485,175 L484,175 L483,176 L481,176 L481,176 L479,176 L478,176 L478,177 L477,177 M499,148 L499,148 L499,149 L499,152 L499,155 L499,156 L499,159 L499,160 L499,162 L499,164 L499,165 L499,167 L499,168 L499,169 L499,170 L499,171 L499,173 L499,174 L499,176 L499,176 L499,177 L499,178 M499,148 L499,148 L500,148 L501,148 L501,148 L502,148 L505,148 L506,148 L507,148 L508,148 L509,148 L509,148 L510,148 L511,148 L512,148 L513,148 L513,148 L515,148 L516,148 L517,148 M498,157 L498,157 L499,157 L502,158 L508,158 L510,158 L512,158 L514,158 L515,158 L517,158 M520,148 L520,148 L519,148 L519,149 L519,151 L521,155 L521,156 L521,157 L521,158 L521,160 L522,161 L522,163 L522,164 L522,164 L523,166 L523,167 L523,168 L523,168 L523,169 L524,170 L524,172 L525,173 L525,173 L526,176 L527,176 L528,176 M537,145 L537,145 L537,147 L537,149 L537,151 L537,152 L537,156 L537,157 L537,159 L537,161 L537,162 L537,164 L536,167 L536,168 L535,169 L534,171 L534,172 L534,173 L533,173 L532,175 L532,176 L531,176 L531,177 L530,177 L529,178 L529,179 M545,148 L545,148 L545,151 L545,154 L545,157 L545,160 L545,164 L545,167 L545,169 L545,170 L545,171 L545,172 L545,172 M540,145 L540,145 L541,145 L542,144 L544,144 L545,144 L547,144 L548,144 M541,176 L541,176 L542,176 L543,176 L545,176 L545,174 L547,174 L548,174 L549,174 L550,174 L550,173 M553,144 L553,144 L553,145 L553,146 L553,148 L554,150 L554,152 L554,155 L554,156 L554,158 L554,160 L554,162 L554,164 L554,166 L554,168 L554,170 L554,171 L554,172 L554,172 L554,173 L554,174 M553,147 L553,147 L553,146 L554,146 L557,146 L557,146 L560,146 L561,146 L562,146 M553,165 L553,165 L554,165 L557,164 L559,164 L561,164 L562,163 L563,163 M559,148 L559,148 L560,148 L561,148 L562,148 L564,148 L565,148 L565,148 L567,148 M552,175 L552,175 L552,176 L553,176 L555,176 L557,175 L558,175 L560,175 L561,175 L561,175 L562,175 L563,175 M569,145 L569,145 L572,145 L573,149 L574,152 L574,155 L575,157 L575,159 L577,160 L577,161 L577,162 L577,163 L577,164 L577,166 L578,167 L579,168 L579,169 L579,170 M585,152 L585,152 L584,154 L584,155 L583,158 L583,159 L583,161 L583,163 L582,165 L582,166 L581,168 L581,169 L580,170 L580,172 L580,173 L580,174 L580,175 L580,176 M585,153 L585,153 L586,156 L586,158 L587,160 L587,162 L588,165 L588,167 L588,168 L589,171 L589,172 L589,172 L590,173 L591,174 M596,144 L596,144 L596,146 L596,148 L596,151 L596,154 L596,156 L596,156 L596,158 L596,159 L595,161 L595,163 L594,166 L593,167 L593,168 L593,170 L592,171 L592,173 L592,175 L590,177 M603,145 L603,145 L603,147 L603,149 L603,152 L605,153 L605,156 L605,156 L605,158 L605,160 L605,162 L605,165 L605,167 L605,168 L605,168 L605,169 M603,147 L603,147 L604,146 L605,145 L605,145 L608,145 L610,145 L611,145 L613,145 L614,145 L616,145 L617,145 L617,145 L619,145 L620,145 L621,145 L622,145 L623,145 L625,145 L625,145 M605,160 L605,160 L607,160 L610,160 L613,160 L613,160 L614,160 L616,160 L614,160 M603,163 L603,163 L603,164 L603,164 L603,167 L603,168 L603,170 L603,171 L603,172 L603,172 L603,173 L603,174 L603,175 L603,176 L603,176 L604,176 L605,176 L606,176 L608,176 L610,176 L611,175 L613,174 L614,174 L615,173 L617,173 L617,172 L618,172 M629,146 L629,146 L628,147 L628,150 L628,153 L628,158 L628,161 L628,164 L628,165 L628,167 L628,168 L628,169 L628,170 L628,171 L628,172 L628,172 L627,173 M628,144 L628,144 L629,144 L631,144 L633,144 L633,144 L634,144 L635,144 L636,144 L637,144 L637,144 L639,145 L640,147 L640,148 L640,149 L640,151 L640,152 L640,153 L640,154 L640,156 L640,156 L640,158 L640,159 L640,160 L638,160 L636,162 L634,162 L633,163 L631,164 L630,164 L629,164 L628,164 L627,164 L627,165 L628,165 L629,166 L629,166 L630,166 L631,167 L632,168 L633,168 L633,169 L634,170 L634,171 L634,172 L634,172 L635,173 L636,174 M626,172 L626,172 L626,173 L626,174 M634,172 L634,172 L635,174 L636,175 L637,176 L637,176 L637,177 M626,171 L626,171 L626,172 L626,173 L626,174 L626,175 L626,176 L626,176 L626,177 L626,179";
            }
            else if (args.Value.ToString() == "Star")
            {
                selectedAnnotation.Data = "M243.75,108 L243.75,108 L244.5,108 L246,108 L246.75,108 L247.5,107.25 L248.25,107.25 L249.75,107.25 L250.5,107.25 L251.25,107.25 L252.75,107.25 L252.75,107.25 L254.25,107.25 L255.75,107.25 L255.75,107.25 L257.25,107.25 L258.75,107.25 L259.5,107.25 L261.75,107.25 L261.75,107.25 L263.25,107.25 L264.75,107.25 L266.25,107.25 L267.75,107.25 L268.5,107.25 L270,107.25 L270.75,107.25 L271.5,107.25 L272.25,107.25 L273,107.25 L273.75,107.25 L274.5,107.25 L275.25,107.25 L276.75,107.25 L276.75,107.25 L277.5,107.25 L279,107.25 L279.75,107.25 L280.5,107.25 L281.25,107.25 L282,107.25 L282.75,107.25 L282,107.25 L281.25,108 L279.75,108.75 L279,109.5 L277.5,110.25 L276.75,110.25 L275.25,111 L273.75,112.5 L272.25,113.25 L271.5,113.25 L270,115.5 L269.25,115.5 L269.25,116.25 L267.75,116.25 L266.25,117.75 L264.75,117.75 L264.75,118.5 L263.25,119.25 L261.75,120 L261,120 L260.25,120.75 L258.75,121.5 L258,122.25 L258,122.25 L255.75,123.75 L254.25,124.5 L253.5,125.25 L252,125.25 L251.25,126 L250.5,126.75 L249.75,127.5 L249,127.5 L248.25,127.5 L248.25,126.75 L248.25,126 L249,125.25 L249.75,125.25 L249.75,124.5 L249.75,123.75 L249.75,123 L250.5,122.25 L251.25,122.25 L252,120.75 L252,120 L252.75,120 L252.75,118.5 L254.25,117.75 L254.25,116.25 L255.75,115.5 L255.75,114.75 L256.5,113.25 L258,111 L258.75,110.25 L258.75,109.5 L258.75,108 L258.75,107.25 L258.75,107.25 L260.25,106.5 L261,105.75 L261,104.25 L261.75,103.5 L262.5,102 L262.5,101.25 L263.25,100.5 L263.25,99.75 L263.25,99 L264,98.25 L264,98.25 L264,97.5 L264.75,96 L264.75,95.25 L264.75,95.25 L264.75,94.5 L265.5,94.5 L266.25,95.25 L267,96 L267.75,96.75 L267.75,98.25 L267.75,99.75 L269.25,101.25 L269.25,101.25 L270,102 L270,102.75 L270,103.5 L270,104.25 L270,105.75 L270.75,106.5 L270.75,107.25 L270.75,108 L271.5,109.5 L271.5,110.25 L271.5,111 L271.5,111.75 L271.5,112.5 L271.5,113.25 L271.5,114.75 L272.25,115.5 L272.25,116.25 L272.25,117 L273,118.5 L273,119.25 L273,119.25 L273.75,120.75 L273.75,121.5 L273.75,122.25 L274.5,123 L274.5,123.75 L275.25,124.5 L275.25,125.25 L275.25,125.25 L276,126 L276.75,126.75 L276.75,127.5 L276,127.5 L273.75,126.75 L273.75,126 L272.25,126 L270.75,125.25 L269.25,124.5 L267.75,123.75 L266.25,122.25 L265.5,122.25 L264.75,121.5 L264,120.75 L261.75,120 L261.75,119.25 L261.75,119.25 L259.5,117.75 L259.5,117 L258,116.25 L258,116.25 L257.25,115.5 L256.5,114.75 L255.75,114 L255.75,114 L255,113.25 L253.5,113.25 L253.5,112.5 L252.75,112.5 L252.75,111.75 L252,111.75 L251.25,111.75 L250.5,111 L249.75,110.25 L249.75,110.25 L249,110.25 L249,110.25 L247.5,110.25 L246.75,109.5 L246.75,108.75 L246,108.75 L245.25,108.75";
            }
            else
            {
                selectedAnnotation.Data = "[{\"command\":\"M\",\"x\":244.83334350585938,\"y\":982.0000305175781},{\"command\":\"L\",\"x\":244.83334350585938,\"y\":982.0000305175781},{\"command\":\"L\",\"x\":250.83334350585938,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":252.83334350585938,\"y\":946.0000305175781},{\"command\":\"L\",\"x\":254.16668701171875,\"y\":940.6667175292969},{\"command\":\"L\",\"x\":256.8333435058594,\"y\":931.3333435058594},{\"command\":\"L\",\"x\":257.5,\"y\":929.3333435058594},{\"command\":\"L\",\"x\":258.8333435058594,\"y\":926.6667175292969},{\"command\":\"L\",\"x\":259.5,\"y\":924.0000305175781},{\"command\":\"L\",\"x\":259.5,\"y\":922.6667175292969},{\"command\":\"L\",\"x\":258.8333435058594,\"y\":922.0000305175781},{\"command\":\"L\",\"x\":258.16668701171875,\"y\":922.0000305175781},{\"command\":\"L\",\"x\":256.8333435058594,\"y\":922.0000305175781},{\"command\":\"L\",\"x\":256.16668701171875,\"y\":922.6667175292969},{\"command\":\"L\",\"x\":254.83334350585938,\"y\":923.3333435058594},{\"command\":\"L\",\"x\":254.16668701171875,\"y\":923.3333435058594},{\"command\":\"L\",\"x\":253.5,\"y\":923.3333435058594},{\"command\":\"L\",\"x\":252.83334350585938,\"y\":925.3333435058594},{\"command\":\"L\",\"x\":252.83334350585938,\"y\":927.3333435058594},{\"command\":\"L\",\"x\":252.83334350585938,\"y\":936.0000305175781},{\"command\":\"L\",\"x\":253.5,\"y\":940.6667175292969},{\"command\":\"L\",\"x\":254.83334350585938,\"y\":944.6667175292969},{\"command\":\"L\",\"x\":260.16668701171875,\"y\":952.0000305175781},{\"command\":\"L\",\"x\":264.16668701171875,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":274.16668701171875,\"y\":958.6667175292969},{\"command\":\"L\",\"x\":278.16668701171875,\"y\":960.0000305175781},{\"command\":\"L\",\"x\":281.5,\"y\":961.3333435058594},{\"command\":\"L\",\"x\":285.5,\"y\":964.6667175292969},{\"command\":\"L\",\"x\":286.8333740234375,\"y\":967.3333435058594},{\"command\":\"L\",\"x\":286.8333740234375,\"y\":970.0000305175781},{\"command\":\"L\",\"x\":282.8333740234375,\"y\":978.6667175292969},{\"command\":\"L\",\"x\":278.16668701171875,\"y\":983.3333435058594},{\"command\":\"L\",\"x\":266.16668701171875,\"y\":991.3333435058594},{\"command\":\"L\",\"x\":259.5,\"y\":993.3333435058594},{\"command\":\"L\",\"x\":252.16668701171875,\"y\":994.0000305175781},{\"command\":\"L\",\"x\":240.83334350585938,\"y\":991.3333435058594},{\"command\":\"L\",\"x\":236.16668701171875,\"y\":988.6667175292969},{\"command\":\"L\",\"x\":230.16668701171875,\"y\":982.6667175292969},{\"command\":\"L\",\"x\":228.83334350585938,\"y\":980.6667175292969},{\"command\":\"L\",\"x\":228.16668701171875,\"y\":978.6667175292969},{\"command\":\"L\",\"x\":228.83334350585938,\"y\":974.6667175292969},{\"command\":\"L\",\"x\":230.16668701171875,\"y\":973.3333435058594},{\"command\":\"L\",\"x\":236.16668701171875,\"y\":971.3333435058594},{\"command\":\"L\",\"x\":240.83334350585938,\"y\":971.3333435058594},{\"command\":\"L\",\"x\":246.16668701171875,\"y\":972.0000305175781},{\"command\":\"L\",\"x\":257.5,\"y\":974.6667175292969},{\"command\":\"L\",\"x\":262.8333435058594,\"y\":976.0000305175781},{\"command\":\"L\",\"x\":269.5,\"y\":977.3333435058594},{\"command\":\"L\",\"x\":276.16668701171875,\"y\":978.6667175292969},{\"command\":\"L\",\"x\":279.5,\"y\":978.0000305175781},{\"command\":\"L\",\"x\":285.5,\"y\":976.6667175292969},{\"command\":\"L\",\"x\":288.16668701171875,\"y\":974.6667175292969},{\"command\":\"L\",\"x\":292.8333740234375,\"y\":969.3333435058594},{\"command\":\"L\",\"x\":293.5,\"y\":966.6667175292969},{\"command\":\"L\",\"x\":294.16668701171875,\"y\":964.0000305175781},{\"command\":\"L\",\"x\":293.5,\"y\":960.0000305175781},{\"command\":\"L\",\"x\":293.5,\"y\":958.0000305175781},{\"command\":\"L\",\"x\":292.8333740234375,\"y\":956.6667175292969},{\"command\":\"L\",\"x\":291.5,\"y\":954.6667175292969},{\"command\":\"L\",\"x\":291.5,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":291.5,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":291.5,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":292.16668701171875,\"y\":954.6667175292969},{\"command\":\"L\",\"x\":292.8333740234375,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":294.16668701171875,\"y\":961.3333435058594},{\"command\":\"L\",\"x\":295.5,\"y\":964.6667175292969},{\"command\":\"L\",\"x\":297.5,\"y\":969.3333435058594},{\"command\":\"L\",\"x\":298.8333740234375,\"y\":970.6667175292969},{\"command\":\"L\",\"x\":301.5,\"y\":970.0000305175781},{\"command\":\"L\",\"x\":304.16668701171875,\"y\":968.6667175292969},{\"command\":\"L\",\"x\":305.5,\"y\":966.0000305175781},{\"command\":\"L\",\"x\":308.8333740234375,\"y\":960.0000305175781},{\"command\":\"L\",\"x\":310.16668701171875,\"y\":957.3333435058594},{\"command\":\"L\",\"x\":310.8333740234375,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":310.8333740234375,\"y\":954.6667175292969},{\"command\":\"L\",\"x\":310.8333740234375,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":311.5,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":312.8333740234375,\"y\":959.3333435058594},{\"command\":\"L\",\"x\":316.16668701171875,\"y\":968.0000305175781},{\"command\":\"L\",\"x\":317.5,\"y\":972.6667175292969},{\"command\":\"L\",\"x\":318.16668701171875,\"y\":977.3333435058594},{\"command\":\"L\",\"x\":319.5,\"y\":983.3333435058594},{\"command\":\"L\",\"x\":319.5,\"y\":986.0000305175781},{\"command\":\"L\",\"x\":319.5,\"y\":988.0000305175781},{\"command\":\"L\",\"x\":318.8333740234375,\"y\":988.0000305175781},{\"command\":\"L\",\"x\":318.16668701171875,\"y\":988.6667175292969},{\"command\":\"L\",\"x\":316.16668701171875,\"y\":987.3333435058594},{\"command\":\"L\",\"x\":314.8333740234375,\"y\":985.3333435058594},{\"command\":\"L\",\"x\":314.16668701171875,\"y\":980.6667175292969},{\"command\":\"L\",\"x\":314.8333740234375,\"y\":974.6667175292969},{\"command\":\"L\",\"x\":316.16668701171875,\"y\":969.3333435058594},{\"command\":\"L\",\"x\":319.5,\"y\":960.6667175292969},{\"command\":\"L\",\"x\":320.16668701171875,\"y\":957.3333435058594},{\"command\":\"L\",\"x\":321.5,\"y\":955.3333435058594},{\"command\":\"L\",\"x\":322.16668701171875,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":322.8333740234375,\"y\":952.6667175292969},{\"command\":\"L\",\"x\":324.16668701171875,\"y\":952.6667175292969},{\"command\":\"L\",\"x\":324.8333740234375,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":326.8333740234375,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":328.16668701171875,\"y\":958.0000305175781},{\"command\":\"L\",\"x\":328.8333740234375,\"y\":960.0000305175781},{\"command\":\"L\",\"x\":329.5,\"y\":962.0000305175781},{\"command\":\"L\",\"x\":330.16668701171875,\"y\":962.0000305175781},{\"command\":\"L\",\"x\":330.16668701171875,\"y\":962.6667175292969},{\"command\":\"L\",\"x\":330.16668701171875,\"y\":962.0000305175781},{\"command\":\"L\",\"x\":330.8333740234375,\"y\":960.0000305175781},{\"command\":\"L\",\"x\":331.5,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":332.8333740234375,\"y\":952.0000305175781},{\"command\":\"L\",\"x\":333.5,\"y\":950.0000305175781},{\"command\":\"L\",\"x\":334.8333740234375,\"y\":948.6667175292969},{\"command\":\"L\",\"x\":335.5,\"y\":948.6667175292969},{\"command\":\"L\",\"x\":336.16668701171875,\"y\":948.6667175292969},{\"command\":\"L\",\"x\":337.5,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":338.8333740234375,\"y\":952.0000305175781},{\"command\":\"L\",\"x\":340.8333740234375,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":341.5,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":342.8333740234375,\"y\":954.6667175292969},{\"command\":\"L\",\"x\":344.8333740234375,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":346.8333740234375,\"y\":952.6667175292969},{\"command\":\"L\",\"x\":349.5,\"y\":949.3333435058594},{\"command\":\"L\",\"x\":350.8333740234375,\"y\":948.0000305175781},{\"command\":\"L\",\"x\":351.5,\"y\":946.6667175292969},{\"command\":\"L\",\"x\":352.8333740234375,\"y\":944.0000305175781},{\"command\":\"L\",\"x\":352.8333740234375,\"y\":943.3333435058594},{\"command\":\"L\",\"x\":354.16668701171875,\"y\":942.0000305175781},{\"command\":\"L\",\"x\":354.8333740234375,\"y\":942.0000305175781},{\"command\":\"L\",\"x\":354.8333740234375,\"y\":942.6667175292969},{\"command\":\"L\",\"x\":354.16668701171875,\"y\":943.3333435058594},{\"command\":\"L\",\"x\":354.16668701171875,\"y\":946.6667175292969},{\"command\":\"L\",\"x\":354.16668701171875,\"y\":950.0000305175781},{\"command\":\"L\",\"x\":355.5,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":356.16668701171875,\"y\":957.3333435058594},{\"command\":\"L\",\"x\":358.16668701171875,\"y\":959.3333435058594},{\"command\":\"L\",\"x\":360.16668701171875,\"y\":958.0000305175781},{\"command\":\"L\",\"x\":364.16668701171875,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":370.8333740234375,\"y\":948.6667175292969},{\"command\":\"L\",\"x\":373.5,\"y\":943.3333435058594},{\"command\":\"L\",\"x\":375.5,\"y\":937.3333435058594},{\"command\":\"L\",\"x\":376.16668701171875,\"y\":933.3333435058594},{\"command\":\"L\",\"x\":376.8333740234375,\"y\":931.3333435058594},{\"command\":\"L\",\"x\":376.8333740234375,\"y\":930.0000305175781},{\"command\":\"L\",\"x\":376.8333740234375,\"y\":929.3333435058594},{\"command\":\"L\",\"x\":376.16668701171875,\"y\":930.0000305175781},{\"command\":\"L\",\"x\":375.5,\"y\":932.0000305175781},{\"command\":\"L\",\"x\":375.5,\"y\":937.3333435058594},{\"command\":\"L\",\"x\":374.8333740234375,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":374.8333740234375,\"y\":960.6667175292969},{\"command\":\"L\",\"x\":375.5,\"y\":966.0000305175781},{\"command\":\"L\",\"x\":377.5,\"y\":974.6667175292969},{\"command\":\"L\",\"x\":378.16668701171875,\"y\":977.3333435058594},{\"command\":\"L\",\"x\":380.8333740234375,\"y\":981.3333435058594},{\"command\":\"L\",\"x\":382.16668701171875,\"y\":982.6667175292969},{\"command\":\"L\",\"x\":383.5,\"y\":982.6667175292969},{\"command\":\"L\",\"x\":387.5,\"y\":982.6667175292969},{\"command\":\"L\",\"x\":389.5,\"y\":980.6667175292969},{\"command\":\"L\",\"x\":392.16668701171875,\"y\":976.6667175292969},{\"command\":\"L\",\"x\":392.8333740234375,\"y\":973.3333435058594},{\"command\":\"L\",\"x\":392.16668701171875,\"y\":970.0000305175781},{\"command\":\"L\",\"x\":388.8333740234375,\"y\":965.3333435058594},{\"command\":\"L\",\"x\":385.5,\"y\":964.0000305175781},{\"command\":\"L\",\"x\":382.8333740234375,\"y\":964.0000305175781},{\"command\":\"L\",\"x\":377.5,\"y\":964.0000305175781},{\"command\":\"L\",\"x\":375.5,\"y\":964.6667175292969},{\"command\":\"L\",\"x\":373.5,\"y\":965.3333435058594},{\"command\":\"L\",\"x\":374.8333740234375,\"y\":963.3333435058594},{\"command\":\"L\",\"x\":376.8333740234375,\"y\":961.3333435058594},{\"command\":\"L\",\"x\":382.16668701171875,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":384.16668701171875,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":387.5,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":388.16668701171875,\"y\":952.0000305175781},{\"command\":\"L\",\"x\":388.16668701171875,\"y\":952.6667175292969},{\"command\":\"L\",\"x\":388.8333740234375,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":388.8333740234375,\"y\":954.6667175292969},{\"command\":\"L\",\"x\":389.5,\"y\":959.3333435058594},{\"command\":\"L\",\"x\":389.5,\"y\":960.6667175292969},{\"command\":\"L\",\"x\":390.16668701171875,\"y\":961.3333435058594},{\"command\":\"L\",\"x\":390.8333740234375,\"y\":960.6667175292969},{\"command\":\"L\",\"x\":393.5,\"y\":958.0000305175781},{\"command\":\"L\",\"x\":396.8333740234375,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":398.16668701171875,\"y\":952.0000305175781},{\"command\":\"L\",\"x\":400.16668701171875,\"y\":949.3333435058594},{\"command\":\"L\",\"x\":400.16668701171875,\"y\":948.6667175292969},{\"command\":\"L\",\"x\":400.8333740234375,\"y\":948.0000305175781},{\"command\":\"L\",\"x\":400.8333740234375,\"y\":947.3333435058594},{\"command\":\"L\",\"x\":401.5,\"y\":948.0000305175781},{\"command\":\"L\",\"x\":402.16668701171875,\"y\":949.3333435058594},{\"command\":\"L\",\"x\":403.5,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":404.8333740234375,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":406.16668701171875,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":407.5,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":410.16668701171875,\"y\":952.0000305175781},{\"command\":\"L\",\"x\":412.16668701171875,\"y\":949.3333435058594},{\"command\":\"L\",\"x\":414.16668701171875,\"y\":944.6667175292969},{\"command\":\"L\",\"x\":414.16668701171875,\"y\":942.0000305175781},{\"command\":\"L\",\"x\":414.16668701171875,\"y\":940.6667175292969},{\"command\":\"L\",\"x\":414.16668701171875,\"y\":938.6667175292969},{\"command\":\"L\",\"x\":414.16668701171875,\"y\":938.0000305175781},{\"command\":\"L\",\"x\":415.5,\"y\":939.3333435058594},{\"command\":\"L\",\"x\":418.8333740234375,\"y\":942.6667175292969},{\"command\":\"L\",\"x\":420.16668701171875,\"y\":945.3333435058594},{\"command\":\"L\",\"x\":421.5,\"y\":946.6667175292969},{\"command\":\"L\",\"x\":422.8333740234375,\"y\":950.0000305175781},{\"command\":\"L\",\"x\":423.5,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":423.5,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":422.8333740234375,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":421.5,\"y\":955.3333435058594},{\"command\":\"L\",\"x\":421.5,\"y\":956.0000305175781},{\"command\":\"L\",\"x\":422.16668701171875,\"y\":954.6667175292969},{\"command\":\"L\",\"x\":422.8333740234375,\"y\":954.0000305175781},{\"command\":\"L\",\"x\":424.8333740234375,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":425.5,\"y\":948.6667175292969},{\"command\":\"L\",\"x\":428.16668701171875,\"y\":945.3333435058594},{\"command\":\"L\",\"x\":428.8333740234375,\"y\":943.3333435058594},{\"command\":\"L\",\"x\":428.8333740234375,\"y\":942.6667175292969},{\"command\":\"L\",\"x\":428.8333740234375,\"y\":943.3333435058594},{\"command\":\"L\",\"x\":428.8333740234375,\"y\":945.3333435058594},{\"command\":\"L\",\"x\":428.8333740234375,\"y\":948.0000305175781},{\"command\":\"L\",\"x\":428.8333740234375,\"y\":950.0000305175781},{\"command\":\"L\",\"x\":429.5,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":430.16668701171875,\"y\":953.3333435058594},{\"command\":\"L\",\"x\":432.8333740234375,\"y\":952.6667175292969},{\"command\":\"L\",\"x\":434.8333740234375,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":437.5,\"y\":948.6667175292969},{\"command\":\"L\",\"x\":440.16668701171875,\"y\":944.6667175292969},{\"command\":\"L\",\"x\":441.5,\"y\":942.6667175292969},{\"command\":\"L\",\"x\":442.16668701171875,\"y\":942.0000305175781},{\"command\":\"L\",\"x\":442.8333740234375,\"y\":941.3333435058594},{\"command\":\"L\",\"x\":442.8333740234375,\"y\":942.0000305175781},{\"command\":\"L\",\"x\":442.8333740234375,\"y\":943.3333435058594},{\"command\":\"L\",\"x\":442.8333740234375,\"y\":944.6667175292969},{\"command\":\"L\",\"x\":442.8333740234375,\"y\":946.0000305175781},{\"command\":\"L\",\"x\":443.5,\"y\":949.3333435058594},{\"command\":\"L\",\"x\":444.16668701171875,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":445.5,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":447.5,\"y\":950.6667175292969},{\"command\":\"L\",\"x\":450.16668701171875,\"y\":948.6667175292969},{\"command\":\"L\",\"x\":452.16668701171875,\"y\":945.3333435058594},{\"command\":\"L\",\"x\":453.5,\"y\":942.6667175292969},{\"command\":\"L\",\"x\":452.8333740234375,\"y\":938.6667175292969},{\"command\":\"L\",\"x\":452.16668701171875,\"y\":937.3333435058594},{\"command\":\"L\",\"x\":450.8333740234375,\"y\":936.6667175292969},{\"command\":\"L\",\"x\":448.8333740234375,\"y\":936.0000305175781},{\"command\":\"L\",\"x\":447.5,\"y\":936.6667175292969},{\"command\":\"L\",\"x\":446.16668701171875,\"y\":937.3333435058594},{\"command\":\"L\",\"x\":445.5,\"y\":938.6667175292969},{\"command\":\"L\",\"x\":445.5,\"y\":939.3333435058594},{\"command\":\"L\",\"x\":446.16668701171875,\"y\":939.3333435058594},{\"command\":\"L\",\"x\":446.8333740234375,\"y\":939.3333435058594},{\"command\":\"L\",\"x\":452.16668701171875,\"y\":937.3333435058594},{\"command\":\"L\",\"x\":454.8333740234375,\"y\":936.6667175292969},{\"command\":\"L\",\"x\":456.8333740234375,\"y\":936.0000305175781},{\"command\":\"L\",\"x\":459.5,\"y\":936.6667175292969},{\"command\":\"L\",\"x\":460.8333740234375,\"y\":937.3333435058594},{\"command\":\"L\",\"x\":461.5,\"y\":938.6667175292969},{\"command\":\"L\",\"x\":462.16668701171875,\"y\":942.0000305175781},{\"command\":\"L\",\"x\":462.16668701171875,\"y\":942.6667175292969},{\"command\":\"L\",\"x\":462.16668701171875,\"y\":944.0000305175781},{\"command\":\"L\",\"x\":462.16668701171875,\"y\":943.3333435058594},{\"command\":\"L\",\"x\":462.16668701171875,\"y\":942.6667175292969},{\"command\":\"L\",\"x\":462.16668701171875,\"y\":941.3333435058594},{\"command\":\"L\",\"x\":462.8333740234375,\"y\":938.6667175292969},{\"command\":\"L\",\"x\":464.16668701171875,\"y\":935.3333435058594},{\"command\":\"L\",\"x\":465.5,\"y\":933.3333435058594},{\"command\":\"L\",\"x\":466.16668701171875,\"y\":932.6667175292969},{\"command\":\"L\",\"x\":467.5,\"y\":933.3333435058594},{\"command\":\"L\",\"x\":469.5,\"y\":935.3333435058594},{\"command\":\"L\",\"x\":470.16668701171875,\"y\":938.6667175292969},{\"command\":\"L\",\"x\":472.8333740234375,\"y\":943.3333435058594},{\"command\":\"L\",\"x\":472.8333740234375,\"y\":944.6667175292969},{\"command\":\"L\",\"x\":474.16668701171875,\"y\":944.6667175292969},{\"command\":\"L\",\"x\":475.5,\"y\":944.0000305175781},{\"command\":\"L\",\"x\":478.16668701171875,\"y\":941.3333435058594},{\"command\":\"L\",\"x\":481.5,\"y\":937.3333435058594},{\"command\":\"L\",\"x\":484.8333740234375,\"y\":934.0000305175781},{\"command\":\"L\",\"x\":488.8333740234375,\"y\":929.3333435058594},{\"command\":\"L\",\"x\":489.5,\"y\":928.0000305175781}]";
            }
        }
        //Method for update the input is changed
        internal void InputChange()
        {
            selectedAnnotation.IsInputChange = true;
        }

        //Method for create the new reply comment
        internal void AddReply()
        {
            selectedAnnotation.IsInputChange = true;
            if (selectedAnnotation.Comment == null || selectedAnnotation.Comment == "")
            {
                selectedAnnotation.Comment = "New Comment";
            }
            Comment comment = new Comment();
            comment.Id = GetRandomId();
            comment.Author = selectedAnnotation.ReplyAuthor;
            comment.Note = selectedAnnotation.ReplyComment;
            comment.ModifiedDate = DateTime.Now.ToString();
            comment.State = selectedAnnotation.ReplyState;
            selectedAnnotation.ReplyComments.Add(comment);
            selectedAnnotation.ReplyAuthor = "Guest";
            selectedAnnotation.ReplyState = "";
            selectedAnnotation.ReplyComment = "Reply Comment";
            StateHasChanged();
        }
        //Method for Image change in custom stamp
        internal async void ImageChane(UploadChangeEventArgs action)
        {
            string base64 = action.Files[0].File.ToString()!;
            string type = action.Files[0].FileInfo.Type.ToLower();
            MemoryStream fileStream = new MemoryStream();
            await action.Files[0].File.OpenReadStream(long.MaxValue).CopyToAsync(fileStream);
            string baseString = Convert.ToBase64String(fileStream.ToArray());
            selectedAnnotation.CustomStampSource = "data:image/jpeg;base64," + baseString;
            byte[] bytes = Convert.FromBase64String(Convert.ToBase64String(fileStream.ToArray()));
            fileStream.Close();
            StateHasChanged();

        }
        //Method for the image remove from the file uploader
        internal async Task OnFileRemove()
        {
            selectedAnnotation.CustomStampSource = "data:image/jpeg;base64,iVBORw0KGgoAAAANSUhEUgAAAMwAAADJCAMAAABYMS1zAAAAllBMVEX///8rNXz2kh7b3OXQ0d8jLnlMU4wfK3hYXpH5+fvz8/bv7/SRlbYAAGyLj7GMkLBGToqdoLt4fKS1t8vIydoAGHEXJXUMHnP1hAD1jAD9483+9u7707L4sXSDh6s0PoL3oE76xpr4qV/97d/5vov96NYADW/2m0b7zaX2mTv4t3v5uYT83cL4rms+R4YAAGX2kjBubpsmc7m7AAAEKUlEQVR4nO3c6VbiQBAF4Dai7AiikCbIjuyK7/9ykziTCJLuVLUcu5O5938d8x2kUr0chEAQBEEQpBgJbD/A9RLMVnPbz3C1LHw5Gdh+iCtl7Xue3AxtP8ZVMpReGOlNC/DF2Xt/I+U295rlRHoxZ5ZzzXyWWELNZGn7eX6SYHdiidrA3vYT/SBT3zuPXNt+JOOsv1tCzSKnX5y9vLB4nr/N5TQw8NIwnjzksA3MV6mWqKnlrg0EM4UlmgbyNtu8KC2fs43tx2NlobFEnDzNNmst5bMN5EYzzLKEmlVOmtpgo/8n+/fFyUVTW6ob2ZlG5qCpBbpGdhbf/QXbjmoJNTvHZ5sp3ZI521S7oxY93biqwSga3Wr+/ppjyZptaq3XMjm9Rox569Or3p/Ufz51UtZqdPs2tYf+DTl3CaZdplc11Zj5kWnxtAs2q5hANSlr4+9UTc0mRjMp6zWqfRubmK2ZJdRs0lu0RQyrKX/TTBzDEKZLVeQxfWPdGoY0XSosqpnTFka55KdgVK8aSxizpvzPolxEW8LMLvf7qPHVS2hrn4ypRreAttYADF8y2qMBe615aoTRHgxYfGkOjwYfjnbpbHOc2U/YCwD9EYfVqZm4lZHE32ktttczW05T818yNjTsYkRwcVim+R+bZG1nWMakHpcpLJvMPU3rGLGXtC+OzL6CYh8jlqT5mbKf6QDm/PRfFcq5swsYEewyLaRTZycwYVPLOGyiHc+4gYkW0bpzwMym7BZG7DUaSdwvdwYTzjaqN45PvRfoDkbMD+kan3zI5BBGBIs0jU8/N3cJk3rqLDMmZXcxlws21pm5Y5jvCzY54xz8uYYRy8PpzcbsSdlpjAhO9m2OvLN/9zDRbBNzmLcaHcQks41cMOucxIh91NTkjFvmJkYMVlJKdpWjmOi8k3+3xFWMCAxuljiLMUmxMKP3Z3JeWzFm3KRX6a6bXDfVpw4jyWNxihq6i0BX1rBiVPVrFgRBkIKkVmekZlSV9OaAEwNLtfvxSE9ye7bFKPpIXpovjBxMBs1Ws0dOPxln3p7pVV/jjGSEvCd7ivnNQZNzUA4MMMAAAwwwwAADDDDAAAMMMMAAAwwwwAADDDDAAAMMMMAAAwwwwPzvmOcCYVoFwlRLXUZKcRmnqJtcBBoysnb+lxkRBEEQSuq3jNTjKk6RYVUt/Xl1qXbuK/R04qpHRlE7mQAYRZV2SfHEmtQajN+e7T/GmArnt2fjC8TipnedX6xVYxhTc3mUYIxms/GdSRUHwxg0gQEGGGCAAQYYYIABBhhggAEGGGCAAQYYYIABBhhggAEGGGCAAaaImELdA3i9I6f3henTq5pfNzTK9CqjGxqdt3ty2g8x5qNNrxonmAq96H5sgBH1EiPJPdhbTlVyq4lTVDK41YQgCIIgCIIgSFr+AKg+KPUzaG6DAAAAAElFTkSuQmCC";
            await uploadObj!.RemoveAsync();
        }

        //Method for Handel the selected item from the context menu
        internal void SelectedHandler(MenuEventArgs<MenuItem> args)
        {
            SelectedItem = args.Item.Text.ToString();
            if (SelectedItem == "Edit")
            {
                OpenReplyComment(selectedAnnotation.ReplyId);
            }
            else if (SelectedItem == "Delete")
            {
                DeleteReplyComment(selectedAnnotation.ReplyId);
            }
        }
        //Method for update the Shape value in the property panel
        internal void ShapeChanged(ChangeEventArgs<string, AnnotationsFields> args)
        {
            if (selectedAnnotation.AnnotationType != null)
            {
                selectedAnnotation.AnnotationType = args.Value.ToString();
            }
            if (selectedAnnotation.AnnotationUnSelected)
            {
                ResetAnnotationProperties(selectedAnnotation);
            }

        }
        //Method for update the stamp type in the property panel
        internal void StampChanged(ChangeEventArgs<string, AnnotationsFields> args)
        {
            if (selectedAnnotation.AnnotationUnSelected)
            {
                ResetAnnotationProperties(selectedAnnotation);
            }
        }
        //Method for update the commnet status value in the property panel
        internal void CommentStatusChanged(ChangeEventArgs<string, AnnotationsFields> args)
        {
            if (args.Value != null)
            {
                selectedAnnotation.State = args.Value.ToString();
            }
        }
        //Method for update the reply status value in the property panel
        internal void ReplyStatusChanged(ChangeEventArgs<string, AnnotationsFields> args)
        {
            if (args.Value != null)
            {
                selectedAnnotation.ReplyState = args.Value.ToString();
            }
        }
        //Method for update the lock value in the property panel
        internal void LockChange(Microsoft.AspNetCore.Components.ChangeEventArgs changeEventArgs)
        {
            selectedAnnotation.IsInputChange = true;
            if (changeEventArgs != null)
            {
                if (changeEventArgs.Value != null && changeEventArgs.Value.ToString() != "")
                {
                    selectedAnnotation.IsLocked = bool.Parse(changeEventArgs.Value.ToString()!);
                }
            }
            if (selectedAnnotation.IsLocked && selectedAnnotation.AllowedInteractions != null)
            {
                if (selectedAnnotation.AllowedInteractions.Count == 0)
                {
                    selectedAnnotation.AllowedInteractions.Add("Select");
                    selectedAnnotation.AllowedInteractions.Add("Resize");
                }
            }
        }
        
        //Method for update the reply in the property panel
        internal void IsAddReplyCommentChange(Microsoft.AspNetCore.Components.ChangeEventArgs changeEventArgs)
        {
            selectedAnnotation.IsReply = bool.Parse(changeEventArgs.Value!.ToString()!);
            if (selectedAnnotation.EditReply)
            {
                selectedAnnotation.EditReply = false;
            }
            if (selectedAnnotation.IsReply == false)
            {
                selectedAnnotation.IsInputChange = true;
                selectedAnnotation.ReplyComments = new List<Comment>();
            }
        }
        //Method for update the print value in the property panel
        internal void PrintChange(Microsoft.AspNetCore.Components.ChangeEventArgs changeEventArgs)
        {
            selectedAnnotation.IsInputChange = true;
            selectedAnnotation.IsPrint = bool.Parse(changeEventArgs.Value!.ToString()!);
        }
    }
}
