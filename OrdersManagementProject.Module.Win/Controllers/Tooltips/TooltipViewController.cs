using System;
using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace OrdersManagementProject.Module.Win.Controllers.Tooltips
{
    /// <summary>
    /// Draws an (i) information icon on detail view field captions, which have a ToolTip text assigned.
    /// </summary>
    public partial class TooltipViewController : ViewController
    {
        public TooltipViewController()
        {
            InitializeComponent();
            this.TargetViewType = ViewType.DetailView;
        }

        #region OnActivated

        protected override void OnActivated()
        {
            base.OnActivated();
            if (this.View is DetailView detailView && detailView.LayoutManager != null)
                ((WinLayoutManager)detailView.LayoutManager).ItemCreated += CheckBoxCaptionController_ItemCreated;
        }

        #endregion

        #region CheckBoxCaptionController_ItemCreated

        private void CheckBoxCaptionController_ItemCreated(object sender, ItemCreatedEventArgs e)
        {
            if (e.ViewItem != null)
            {
                if (e.ViewItem.Control == null)
                    e.ViewItem.ControlCreated += (object s, EventArgs args) => { SetCaptionLocation((LayoutControlItem)e.Item, e.ViewItem.Control, Locations.Right); };
                else
                    SetCaptionLocation((LayoutControlItem)e.Item, e.ViewItem.Control, Locations.Right);
            }
        }

        #endregion

        #region SetCaptionLocation

        /// <summary>
        /// Sets the empty caption (only for boolean properties) and assigns icon if ToolTip exists
        /// </summary>
        /// <param name="layoutItem"></param>
        /// <param name="control"></param>
        /// <param name="location"></param>
        private void SetCaptionLocation(LayoutControlItem layoutItem, object control, Locations location)
        {
            if (layoutItem != null && control != null)
            {
                CheckEdit checkEdit = control as CheckEdit;
                if (checkEdit != null)
                {
                    if (!string.IsNullOrEmpty(checkEdit.ToolTip))
                    {
                        ImageOptionsHelper.AssignImage(layoutItem.ImageOptions, this.AboutInfoImage, new Size(14, 14));
                        checkEdit.Text = "";
                        checkEdit.AutoSizeInLayoutControl = true;
                        layoutItem.TextVisible = true;
                        layoutItem.TextLocation = location;
                        layoutItem.TextAlignMode = TextAlignModeItem.AutoSize;
                    }
                }
                else
                {
                    if (layoutItem != null && !string.IsNullOrEmpty(layoutItem.OptionsToolTip.ToolTip))
                    {
                        ImageOptionsHelper.AssignImage(layoutItem.ImageOptions, this.AboutInfoImage, new Size(14, 14));
                        layoutItem.ImageOptions.Alignment = ContentAlignment.MiddleRight;
                    }
                }
            }
        }

        #endregion

        #region OnDeactivated

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            if (this.View is DetailView detailView && detailView.LayoutManager != null)
                ((WinLayoutManager)detailView.LayoutManager).ItemCreated -= CheckBoxCaptionController_ItemCreated;
        }

        #endregion

        #region AboutInfoImage

        private DevExpress.ExpressApp.Utils.ImageInfo _aboutInfoImage;

        private DevExpress.ExpressApp.Utils.ImageInfo AboutInfoImage
        {
            get
            {
                if (_aboutInfoImage == DevExpress.ExpressApp.Utils.ImageInfo.Empty)
                    _aboutInfoImage = ImageLoader.Instance.GetImageInfo("Action_AboutInfo");
                return _aboutInfoImage;
            }
        }

        #endregion
    }
}
