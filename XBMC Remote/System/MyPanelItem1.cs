using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

using Microsoft.Drawing;

using StedySoft.SenseSDK;
using StedySoft.SenseSDK.DrawingCE;

namespace XBMC_Remote {

    class MyPanelItem1 : SensePanelBase, IDisposable {

        #region Declarations
        private bool _disposed = false;

        private IImage _iimage = null;
        private Size _iimageSize = Size.Empty;
        private string _text1 = string.Empty;
        private SenseAPIs.SenseFont.PanelTextAlignment _text1Alignment = SenseAPIs.SenseFont.PanelTextAlignment.Default;
        private SenseAPIs.SenseFont.PanelTextLineHeight _text1LineHeight = SenseAPIs.SenseFont.PanelTextLineHeight.Default;
        private string _text2 = string.Empty;
        private SenseAPIs.SenseFont.PanelTextAlignment _text2Alignment = SenseAPIs.SenseFont.PanelTextAlignment.Default;
        private string _ThumbnailUrl = string.Empty;
        public XbmcJson.XbmcConnection JsonClient;
        #endregion

        #region Constructor
        public MyPanelItem1() { }
        public MyPanelItem1(string name) { base.Name = name; }

        public override void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            base.Dispose();
        }

        private void Dispose(bool disposing) {
            if (!this._disposed) {
                if (disposing) {
                    if (this._iimage != null) {
                        SenseAPIs.SenseImage.ReleaseIImage(this._iimage);
                        this._iimage = null;
                    }
                }
            }
            this._disposed = true;
        }
        #endregion

        #region Private Methods
        private IImage _getIImageFromResource(string resource)
        {
            IImage iimg;

            using (MemoryStream strm = (MemoryStream)Assembly.GetExecutingAssembly().GetManifestResourceStream("XBMC_Remote.Resources." + resource + ".png"))
            {
                (ImagingFactory.GetImaging()).CreateImageFromBuffer(strm.GetBuffer(), (uint)strm.Length, BufferDisposalFlag.BufferDisposalFlagNone, out iimg);
            }
            return iimg;
        }
        #endregion

        #region Properties
        public override bool ButtonAnimation {
            get { return base.ButtonAnimation; }
            set { base.ButtonAnimation = value; }
        }

        public override List<SenseListControl.ISenseListItem> Children {
            // return null as the Children property is not implemented for this panel
            // NOTE: must return null as opposed to throwing exception for panel Dispose method
            get { return null; }
            // throw exception as the Children property is not implemented for this panel
            set { throw new NotImplementedException(); }
        }

        public override Rectangle ClientRectangle {
            get { return base.ClientRectangle; }
            // remove "SET" as the ClientRectangle property is determined by parent list control ClientRectangle
        }

        public override int Height {
            get { return base.Height; }
            set { base.Height = value; }
        }

        public override int Index {
            get { return base.Index; }
            // remove "SET" as the Index property is determined by parent list control Index
        }

        public string ThumbnailUrl
        {
            get { return this._ThumbnailUrl; }
            set { this._ThumbnailUrl = value; }
        }

        public Microsoft.Drawing.IImage Thumbnail {
            get { return this._iimage; }
            set {
                this._iimage = value;
                this._iimageSize = Size.Empty;
                if (this._iimage != null) {
                    ImageInfo imageInfo = new ImageInfo();
                    this._iimage.GetImageInfo(out imageInfo);
                    this._iimageSize.Width = (int)imageInfo.Width;
                    this._iimageSize.Height = (int)imageInfo.Height;
                }
                if (!base.ClientRectangle.Equals(Rectangle.Empty)) { base.Invalidate(); }
            }
        }

        public string PrimaryText {
            get { return this._text1; }
            set {
                if (value != this._text1) {
                    this._text1 = value;
                    if (!base.ClientRectangle.Equals(Rectangle.Empty)) { base.Invalidate(); }
                }
            }
        }

        public SenseAPIs.SenseFont.PanelTextAlignment PrimaryTextAlignment {
            get { return this._text1Alignment; }
            set {
                if (value != this._text1Alignment) {
                    this._text1Alignment = value;
                    if (!base.ClientRectangle.Equals(Rectangle.Empty)) { base.Invalidate(); }
                }
            }
        }

        public SenseAPIs.SenseFont.PanelTextLineHeight PrimaryTextLineHeight {
            get { return this._text1LineHeight; }
            set {
                if (value != this._text1LineHeight) {
                    this._text1LineHeight = value;
                    if (!base.ClientRectangle.Equals(Rectangle.Empty)) { base.Invalidate(); }
                }
            }
        }

        public string SecondaryText {
            get { return this._text2; }
            set {
                if (value != this._text2) {
                    this._text2 = value;
                    if (!base.ClientRectangle.Equals(Rectangle.Empty)) { base.Invalidate(); }
                }
            }
        }

        public SenseAPIs.SenseFont.PanelTextAlignment SecondaryTextAlignment {
            get { return this._text2Alignment; }
            set {
                if (value != this._text2Alignment) {
                    this._text2Alignment = value;
                    if (!base.ClientRectangle.Equals(Rectangle.Empty)) { base.Invalidate(); }
                }
            }
        }

        public override int Width {
            get { return base.Width; }
            // remove "SET" as the Width property is determined by parent list control Width
        }
        #endregion

        #region Events
        public override void OnRender(System.Drawing.Graphics g) {
            base.OnRender(g);

            if (this.Thumbnail == null && this.ThumbnailUrl != null)
            {
                this.Thumbnail = Functions.GetMovieThumbnail(JsonClient, this._ThumbnailUrl);
            }

            if (this._iimage != null) { SenseAPIs.SenseImage.Panel.Draw.ThumbnailIImage(g, this._iimage, this._iimageSize, base.ClientRectangle); }
            
            SenseAPIs.SenseFont.PanelTextLayout ptl = new SenseAPIs.SenseFont.PanelTextLayout(this._text1, this._text2);
            ptl.Enabled = base.Enabled;
            ptl.PanelRectangle = base.ClientRectangle;
            ptl.Pressed = base.IsPressed;
            ptl.PrimaryTextVerticalAlignment = this._text1Alignment;
            ptl.PrimaryTextLineHeight = this._text1LineHeight;
            ptl.SecondaryTextVerticalAlignment = this._text2Alignment;
            ptl.ThumbnailWidth = this._iimageSize.Width;
            SenseAPIs.SenseFont.Panel.Draw.PanelText(g, ptl);
        }
        #endregion

    }

}
