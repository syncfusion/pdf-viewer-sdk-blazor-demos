#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Blazor.SfPdfViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazorDemos.Pages.Viewer
{
    public partial class ProgrammaticalAnnotationBase
    {
        //Method for Apply annotation properties from property panel to the new Annotation
        internal void AnnotationsPropertyUpdate(PdfAnnotation annotation)
        {
            Bound bound = new Bound();
            bound.X = selectedAnnotation.X;
            bound.Y = selectedAnnotation.Y;
            bound.Width = selectedAnnotation.Width;
            bound.Height = selectedAnnotation.Height;
            annotation.PageNumber = (selectedAnnotation.PageNumber == 0 ? 1 : selectedAnnotation.PageNumber) - 1;
            AnnotationType annotationType;
            if (selectedAnnotation.AnnotationType == "CustomStamp")
            {
                annotationType = Enum.Parse<AnnotationType>("Image");
            }
            else
            {
                annotationType = Enum.Parse<AnnotationType>(selectedAnnotation.AnnotationType);
            }
            annotation.Type = annotationType;
            annotation.Bound = bound;
            selectedAnnotation.VertexPointCount = 0;
            annotation.IsLock = selectedAnnotation.IsLocked;
            if (selectedAnnotation.IsLocked)
            {
                annotation.AllowedInteractions = new List<AllowedInteraction>();
                if (selectedAnnotation.AllowedInteractions != null)
                {
                    foreach (string allowIntraction in selectedAnnotation.AllowedInteractions)
                    {
                        if (allowIntraction == "None")
                        {
                            annotation.AllowedInteractions.Add(AllowedInteraction.None);
                        }
                        else if (allowIntraction == "Select")
                        {
                            annotation.AllowedInteractions.Add(AllowedInteraction.Select);
                        }
                        else if (allowIntraction == "Delete")
                        {
                            annotation.AllowedInteractions.Add(AllowedInteraction.Delete);
                        }
                        else if (allowIntraction == "Move")
                        {
                            annotation.AllowedInteractions.Add(AllowedInteraction.Move);
                        }
                        else if (allowIntraction == "Property Change")
                        {
                            annotation.AllowedInteractions.Add(AllowedInteraction.PropertyChange);
                        }
                        else if (allowIntraction == "Resize")
                        {
                            annotation.AllowedInteractions.Add(AllowedInteraction.Resize);
                        }
                    }
                }
            }
            annotation.Opacity = selectedAnnotation.Opacity / 100;
            annotation.FillColor = selectedAnnotation.FillColor;
            annotation.Thickness = selectedAnnotation.Thickness;
            annotation.StrokeColor = selectedAnnotation.StrokeColor;
            annotation.IsPrint = selectedAnnotation.IsPrint;
            annotation.Author = selectedAnnotation.Author;
            annotation.ModifiedDate = selectedAnnotation.ModifiedDate;
            if (annotation.Type == AnnotationType.Highlight || annotation.Type == AnnotationType.Underline || annotation.Type == AnnotationType.Strikethrough)
            {
                annotation.Color = selectedAnnotation.FillColor;
                annotation.TextMarkupContent = "TEXT";
                if (selectedAnnotation.Bounds.Count == 0)
                {
                    Bound newBound = new Bound();
                    newBound.X = selectedAnnotation.X;
                    newBound.Y = selectedAnnotation.Y;
                    newBound.Width = selectedAnnotation.Width;
                    newBound.Height = selectedAnnotation.Height;
                    selectedAnnotation.Bounds.Add(newBound);
                }
            }
            annotation.Review = new Review();
            annotation.Review.State = selectedAnnotation.State;
            if (annotation.Type == AnnotationType.FreeText)
            {
                annotation.BorderWidth = selectedAnnotation.Thickness;
                annotation.BorderColor = selectedAnnotation.StrokeColor;
                annotation.FontColor = selectedAnnotation.FontColor;
                annotation.DefaultText = selectedAnnotation.DefaultText;
                annotation.DynamicText = selectedAnnotation.DefaultText;
                annotation.FontSize = selectedAnnotation.FontSize;
                annotation.FontFamily = selectedAnnotation.FontFamily;
                annotation.TextAlignment = Enum.Parse<TextAlignment>(selectedAnnotation.Alignment);
                annotation.FontStyle = Enum.Parse<FontStyle>(selectedAnnotation.FontStyle);
            }
            if (annotationType != AnnotationType.Distance && annotationType != AnnotationType.Perimeter && annotationType != AnnotationType.Area && annotationType != AnnotationType.Radius && annotationType != AnnotationType.Volume)
            {
                annotation.Note = selectedAnnotation.Comment;
            }
            if (annotationType == AnnotationType.Distance)
            {
                annotation.LeaderLength = selectedAnnotation.LeaderLength;
            }
            if (annotationType == AnnotationType.Line || annotationType == AnnotationType.Arrow || annotationType == AnnotationType.Distance || annotationType == AnnotationType.Perimeter)
            {
                annotation.LineHeadStart = Enum.Parse<LineHeadStyle>(selectedAnnotation.LindHeadStart);
                annotation.LineHeadEnd = Enum.Parse<LineHeadStyle>(selectedAnnotation.LindHeadEnd);
            }
            if (annotationType == AnnotationType.Ink || annotationType == AnnotationType.HandWrittenSignature)
            {
                annotation.Data = selectedAnnotation.Data;
            }
            VertexPoint vertexPoint = new VertexPoint();
            if (annotationType == AnnotationType.Line || annotationType == AnnotationType.Arrow || annotationType == AnnotationType.Distance)
            {
                annotation.VertexPoints = new List<VertexPoint>();
                vertexPoint.X = selectedAnnotation.VertexX0;
                vertexPoint.Y = selectedAnnotation.VertexY0;
                annotation.VertexPoints.Add(vertexPoint);
                vertexPoint = new VertexPoint();
                vertexPoint.X = selectedAnnotation.VertexX1;
                vertexPoint.Y = selectedAnnotation.VertexY1;
                annotation.VertexPoints.Add(vertexPoint);
            }
            if (annotationType == AnnotationType.Polygon || annotationType == AnnotationType.Perimeter || annotationType == AnnotationType.Area || annotationType == AnnotationType.Volume)
            {
                annotation.VertexPoints = selectedAnnotation.VertexPoints;
            }
            if (selectedAnnotation.Bounds.Count != 0)
            {
                annotation.Bounds = selectedAnnotation.Bounds;
            }
            if (selectedAnnotation.IsReply)
            {
                annotation.Comments = selectedAnnotation.ReplyComments;
            }
            else
            {
                annotation.Comments = new List<Comment>();
            }
            if (annotationType == AnnotationType.Image)
            {
                annotation.CustomStampSource = selectedAnnotation.CustomStampSource;
            }
        }

        //Method for Apply the Common Properties to property panel
        internal void ApplyCommonProperties(PdfAnnotation annotation)
        {
            selectedAnnotation.PageNumber = annotation.PageNumber + 1;
            selectedAnnotation.Opacity = annotation.Opacity * 100;
            selectedAnnotation.IsPrint = annotation.IsPrint;
            selectedAnnotation.IsLocked = annotation.IsLock;
            if (annotation.AllowedInteractions != null)
            {
                if (annotation.AllowedInteractions.Count != 0)
                {
                    foreach (AllowedInteraction AllowedInteraction in annotation.AllowedInteractions)
                    {
                        string allowedInteraction = AllowedInteraction.ToString();

                        if (allowedInteraction == "None")
                        {
                            selectedAnnotation.IsAllowedInterActionsNone = true;
                            selectedAnnotation.AllowedInteractions.Add("None");
                        }
                        if (allowedInteraction == "Select")
                        {
                            selectedAnnotation.IsAllowedInterActionsSelect = true;
                            selectedAnnotation.AllowedInteractions.Add("Select");
                        }
                        if (allowedInteraction == "Move")
                        {
                            selectedAnnotation.IsAllowedInterActionsMove = true;
                            selectedAnnotation.AllowedInteractions.Add("Move");
                        }
                        if (allowedInteraction == "Resize")
                        {
                            selectedAnnotation.IsAllowedInterActionsResize = true;
                            selectedAnnotation.AllowedInteractions.Add("Resize");

                        }
                        if (allowedInteraction == "Delete")
                        {
                            selectedAnnotation.IsAllowedInterActionsDelete = true;
                            selectedAnnotation.AllowedInteractions.Add("Delete");
                        }
                        if (allowedInteraction == "PropertyChange")
                        {
                            selectedAnnotation.IsAllowedInterActionsPropertyChange = true;
                            selectedAnnotation.AllowedInteractions.Add("Property Change");

                        }
                    }
                }
            }
            selectedAnnotation.Author = annotation.Author;
            selectedAnnotation.Comment = annotation.Note;
            selectedAnnotation.State = annotation.Review.State;
            if (annotation.Comments.Count != 0)
            {
                selectedAnnotation.IsReply = true;
                selectedAnnotation.ReplyComments = annotation.Comments;
            }
            else
            {
                selectedAnnotation.IsReply = false;
                selectedAnnotation.ReplyComments = new List<Comment>();
            }
        }
        //Method for Apply the Line Properties to property panel
        internal void ApplyLineProperties(PdfAnnotation annotation)
        {
            selectedAnnotation.LindHeadStart = annotation.LineHeadStart.ToString();
            selectedAnnotation.LindHeadEnd = annotation.LineHeadEnd.ToString();
        }
        //Method for Apply the free text Properties to property panel
        internal void ApplyFreeTextProperties(PdfAnnotation annotation)
        {
            selectedAnnotation.FontFamily = annotation.FontFamily;
            selectedAnnotation.FontSize = annotation.FontSize;
            selectedAnnotation.FontStyle = annotation.FontStyle.ToString();
            selectedAnnotation.FontColor = annotation.FontColor;
            selectedAnnotation.Alignment = annotation.TextAlignment.ToString();
            selectedAnnotation.DefaultText = annotation.DefaultText;

            if (IsRGBAColor(selectedAnnotation.FontColor))
            {
                selectedAnnotation.FontColor = RGBAtoHex(selectedAnnotation.FontColor, "stroke");
            }
        }

        //Method for Apply the Stamp Properties to property panel
        internal void ApplyStampProperties(PdfAnnotation annotation)
        {
            if (annotation.DynamicText != "")
            {
                selectedAnnotation.StampsType = "Dynamic";
                selectedAnnotation.DynamicStamp = annotation.Subject;
            }
            else
            {
                if (selectedAnnotation.SignHereStampList.Contains(annotation.Subject))
                {
                    selectedAnnotation.StampsType = "SignHere";
                    selectedAnnotation.SignHereStamp = annotation.Subject.Replace(" ", "");
                }
                else
                {
                    selectedAnnotation.StampsType = "StandardBusiness";
                    selectedAnnotation.StandardBusinessStamp = annotation.Subject.Replace(" ", "");
                }
            }
        }
        //Method for Apply the shape Properties to property panel
        internal void ApplyShapeProperties(PdfAnnotation annotation)
        {
            selectedAnnotation.X = annotation.Bound.X;
            selectedAnnotation.Y = annotation.Bound.Y;
            selectedAnnotation.Width = annotation.Bound.Width;
            selectedAnnotation.Height = annotation.Bound.Height;
        }
        //Method for Apply the polygon Properties to property panel
        internal void ApplyPolygonProperties(PdfAnnotation annotation)
        {
            selectedAnnotation.VertexPointString = "";
            selectedAnnotation.VertexPointCount = annotation.VertexPoints.Count;
            selectedAnnotation.VertexPoints = new List<VertexPoint>();
            if (selectedAnnotation.VertexPointCount != 0)
            {
                selectedAnnotation.VertexX = annotation.VertexPoints[0].X;
                selectedAnnotation.VertexY = annotation.VertexPoints[0].Y;

                for (int i = 0; i < selectedAnnotation.VertexPointCount; i++)
                {
                    VertexPoint vertexPoint = new VertexPoint();
                    vertexPoint.X = annotation.VertexPoints[i].X;
                    vertexPoint.Y = annotation.VertexPoints[i].Y;
                    selectedAnnotation.VertexPoints.Add(vertexPoint);
                    selectedAnnotation.VertexPointString += ($"X{i + 1}: {vertexPoint.X}   Y{i + 1}: {vertexPoint.Y}\n").ToString();
                }
            }
        }
        //Method for Apply the line and Arrow Properties to property panel
        internal void ApplyVertexProperties(PdfAnnotation annotation)
        {
            if (annotation.VertexPoints.Count != 0)
            {
                selectedAnnotation.VertexX0 = annotation.VertexPoints[0].X;
                selectedAnnotation.VertexY0 = annotation.VertexPoints[0].Y;
                selectedAnnotation.VertexX1 = annotation.VertexPoints[1].X;
                selectedAnnotation.VertexY1 = annotation.VertexPoints[1].Y;
            }
        }

        //Method for checking color is RGBA of Hex code
        internal static bool IsRGBAColor(string input)
        {
            string rgbaPattern = @"rgba\((\d+),\s*(\d+),\s*(\d+),\s*(\d+(?:\.\d+)?)\)";
            if (input != null)
            {
                return Regex.IsMatch(input, rgbaPattern);
            }
            return false;
        }
        //Method to convert the RGBA color to hex code
        internal static string RGBAtoHex(string rgba, string type)
        {
            string[] rgbaValues = rgba.Split(new[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            int r = int.Parse(rgbaValues[1]);
            int g = int.Parse(rgbaValues[2]);
            int b = int.Parse(rgbaValues[3]);
            int a = (int)(double.Parse(rgbaValues[4]) * 255);
            if (type == "stroke")
            {
                return $"#{r:X2}{g:X2}{b:X2}";
            }
            else
            {
                return $"#{r:X2}{g:X2}{b:X2}{a:X2}";
            }
        }

        //Method for getting the random Id for reply comment
        internal string GetRandomId()
        {
            Guid newGUID = Guid.NewGuid();
            return newGUID.ToString();
        }
        //Method for edit the reply comment in pdf vierwer
        internal void UpdateReply()
        {
            selectedAnnotation.IsInputChange = true;
            for (int i = 0; i < selectedAnnotation.ReplyComments.Count; i++)
            {
                if (selectedAnnotation.ReplyComments[i].Id == selectedAnnotation.ReplyId)
                {
                    selectedAnnotation.ReplyComments[i].Author = selectedAnnotation.ReplyAuthor;
                    selectedAnnotation.ReplyComments[i].Note = selectedAnnotation.ReplyComment;
                    selectedAnnotation.ReplyComments[i].State = selectedAnnotation.ReplyState;
                }
            }
            selectedAnnotation.ReplyAuthor = "Guest";
            selectedAnnotation.ReplyState = "";
            selectedAnnotation.ReplyComment = "Reply Comment";
            selectedAnnotation.EditReply = false;
        }
        //Method for delete the reply comment in pdf vierwer
        internal void DeleteReplyComment(string id)
        {
            selectedAnnotation.IsInputChange = true;
            int index = -1;
            for (int i = 0; i < selectedAnnotation.ReplyComments.Count; i++)
            {
                if (selectedAnnotation.ReplyComments[i].Id == id)
                {
                    index = i;
                }
            }
            if (index != -1)
            {
                selectedAnnotation.ReplyComments.RemoveAt(index);
                selectedAnnotation.EditReply = false;
                selectedAnnotation.ReplyAuthor = "Guest";
                selectedAnnotation.ReplyState = "";
                selectedAnnotation.ReplyComment = "Reply Comment";
                selectedAnnotation.EditReply = false;
            }
            if (selectedAnnotation.ReplyComments.Count == 0)
            {
                selectedAnnotation.IsReply = false;
            }
            StateHasChanged();
        }
        //Method for the delete the list for bounds and vertext point
        internal void DeleteList()
        {
            selectedAnnotation.IsInputChange = false;
            if (selectedAnnotation.Bounds.Count != 0)
            {
                selectedAnnotation.Bounds = new List<Bound>();
            }
            if (selectedAnnotation.VertexPoints.Count != 0)
            {
                selectedAnnotation.VertexPoints = new List<VertexPoint>();
            }
        }
        //Method for apply the value of replay comment to property panel
        internal void OpenReplyComment(string id)
        {
            selectedAnnotation.EditReply = true;
            for (int i = 0; i < selectedAnnotation.ReplyComments.Count; i++)
            {
                if (selectedAnnotation.ReplyComments[i].Id == id)
                {
                    selectedAnnotation.ReplyAuthor = selectedAnnotation.ReplyComments[i].Author;
                    selectedAnnotation.ReplyComment = selectedAnnotation.ReplyComments[i].Note;
                    selectedAnnotation.ReplyState = selectedAnnotation.ReplyComments[i].State;
                    selectedAnnotation.ReplyId = selectedAnnotation.ReplyComments[i].Id;
                }
            }
        }
        //Method for Add the bounds value for the text markup annotations
        internal void AddBounds()
        {
            selectedAnnotation.IsInputChange = true;
            Bound bound = new Bound();
            bound.X = selectedAnnotation.X;
            bound.Y = selectedAnnotation.Y;
            bound.Width = selectedAnnotation.Width;
            bound.Height = selectedAnnotation.Height;
            selectedAnnotation.Bounds.Add(bound);
        }
        //Add the vertext points for the polygon type annotations
        internal void AddVertexPoint()
        {
            selectedAnnotation.IsInputChange = true;
            VertexPoint vertexPoint = new VertexPoint();
            vertexPoint.X = selectedAnnotation.VertexX;
            vertexPoint.Y = selectedAnnotation.VertexY;
            selectedAnnotation.VertexPoints.Add(vertexPoint);
            StateHasChanged();
        }

        //Method for save the reply ID
        internal void SaveReplyID(string id)
        {
            selectedAnnotation.ReplyId = id;
        }

        //Method for reset all annotation properties in the property panel
        public void ResetAnnotationProperties(AnnotationBase annotation)
        {
            annotation.VertexPoints = new List<VertexPoint>();
            annotation.Bounds = new List<Bound>();
            string ShapeAnnotation = annotation.AnnotationType;
            if (ShapeAnnotation == "Arrow" || ShapeAnnotation == "Distance")
            {
                annotation.LindHeadStart = "ClosedArrow";
                annotation.LindHeadEnd = "ClosedArrow";
                if (ShapeAnnotation == "Distance")
                {
                    annotation.LeaderLength = 0;
                }
            }
            else if (ShapeAnnotation == "Perimeter")
            {
                annotation.LindHeadEnd = "OpenArrow";
                annotation.LindHeadStart = "OpenArrow";
            }
            else
            {
                annotation.LindHeadEnd = "None";
                annotation.LindHeadStart = "None";
            }
            if (ShapeAnnotation == "Rectangle" || ShapeAnnotation == "Circle" || ShapeAnnotation == "Radius")
            {
                annotation.Width = 100;
                annotation.Height = 100;
            }
            else if (ShapeAnnotation == "Ink")
            {
                annotation.Width = 150;
                annotation.Height = 60;
            }
            else if (ShapeAnnotation == "FreeText")
            {
                annotation.Width = 150;
                annotation.Height = 26.5;
                annotation.FontFamily = "Helvetica";
                annotation.FontStyle = "None";
                annotation.Alignment = "Left";
                annotation.DefaultText = "Free Text";
                annotation.FontSize = 16;
                annotation.FontColor = "#000000";
            }
            else if (ShapeAnnotation == "StickyNotes")
            {
                annotation.Width = 30;
                annotation.Height = 30;
            }
            else if (ShapeAnnotation == "Stamp")
            {
                if (annotation.StampsType == "Dynamic")
                {
                    annotation.Width = 140;
                    annotation.Height = 55;
                }
                if (annotation.StampsType == "SignHere")
                {
                    if (annotation.SignHereStamp == "SignHere")
                    {
                        annotation.Width = 110;
                    }
                    if (annotation.SignHereStamp == "Witness")
                    {
                        annotation.Width = 130;
                    }
                    if (annotation.SignHereStamp == "InitialHere")
                    {
                        annotation.Width = 90;
                    }
                    if (annotation.SignHereStamp == "Accepted" || annotation.SignHereStamp == "Rejected")
                    {
                        annotation.Width = 35;
                        annotation.Height = 35;
                    }
                    else
                    {
                        annotation.Height = 30;
                    }
                }
                if (annotation.StampsType == "StandardBusiness")
                {
                    if (annotation.StandardBusinessStamp == "Final" || annotation.StandardBusinessStamp == "Draft")
                    {
                        annotation.Width = 110;
                    }
                    else if (annotation.StandardBusinessStamp == "Void")
                    {
                        annotation.Width = 100;
                    }
                    else
                    {
                        annotation.Width = 130;
                    }
                    annotation.Height = 30;
                }
            }
            else if (ShapeAnnotation == "CustomStamp")
            {
                annotation.Width = 100;
                annotation.Height = 100;
            }
            else if ((ShapeAnnotation == "Highlight") || (ShapeAnnotation == "Underline") || (ShapeAnnotation == "Strikethrough"))
            {
                annotation.Width = 100;
                annotation.Height = 14;
            }
            else
            {
                annotation.Width = 0;
                annotation.Height = 0;
            }
            annotation.X = 100;
            annotation.Y = 100;
            annotation.FillColor = "#00000000";
            annotation.StrokeColor = "#FF0000";
            if ((ShapeAnnotation == "Highlight") || (ShapeAnnotation == "Underline") || (ShapeAnnotation == "Strikethrough") || ShapeAnnotation == "FreeText")
            {
                annotation.X = 10;
                annotation.Y = 10;
                annotation.StrokeColor = "";
                if (ShapeAnnotation == "Highlight")
                {
                    annotation.FillColor = "#FFFF00";
                }
                else if (ShapeAnnotation == "Underline")
                {
                    annotation.FillColor = "#00FF00";
                }
                else if (ShapeAnnotation == "Strikethrough")
                {
                    annotation.FillColor = "#FF0000";
                }
                else
                {
                    annotation.FillColor = "";
                }
            }
            annotation.Opacity = 100;
            annotation.Thickness = 1;
            annotation.IsPrint = true;
            annotation.IsLocked = false;
            annotation.IsAllowedInterActionsNone = false;
            annotation.IsAllowedInterActionsSelect = false;
            annotation.IsAllowedInterActionsMove = false;
            annotation.IsAllowedInterActionsDelete = false;
            annotation.IsAllowedInterActionsPropertyChange = false;
            annotation.IsAllowedInterActionsResize = false;
            annotation.Author = "Guest";
            annotation.State = "";
            annotation.Comment = "New Comment";
            annotation.IsReply = false;
            annotation.ReplyAuthor = "Guest";
            annotation.ReplyState = "";
            annotation.ReplyComment = "Reply Comment";
            annotation.AllowedInteractions = new List<string>() { };
            annotation.ReplyComments = new List<Comment>();
            annotation.VertexX0 = 100;
            annotation.VertexY0 = 100;
            annotation.VertexX1 = 200;
            annotation.VertexY1 = 100;
            annotation.VertexX = 10;
            annotation.VertexY = 10;
            annotation.VertexPointCount = 0;
            annotation.IsInputChange = false;
            annotation.PageNumber = PdfViewerInstance!.CurrentPageNumber;
        }
    }
}
