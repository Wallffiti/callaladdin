using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallAladdin.UserControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryUserControl : Grid
	{
        private Grid selectedItemGrid;

        public HistoryUserControl ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            ResetTheme();

            if (sender is CachedImage)
            {
                var cachedImage = (CachedImage)sender;

                if (cachedImage.Parent is Grid)
                {
                    selectedItemGrid = (Grid)cachedImage.Parent;
                }
            }
            else if (sender is Grid)
            {
                selectedItemGrid = (Grid)sender;
            }

            if (selectedItemGrid != null)
            {
                selectedItemGrid.BackgroundColor = Color.DarkBlue;
                var categoryLabel = selectedItemGrid.FindByName<Label>("categoryLabel");
                if (categoryLabel != null)
                {
                    categoryLabel.TextColor = Color.White;
                }
                var titleLabel = selectedItemGrid.FindByName<Label>("titleLabel");
                if (titleLabel != null)
                {
                    titleLabel.TextColor = Color.White;
                }
                var modifiedDateTimeLabel = selectedItemGrid.FindByName<Label>("modifiedDateTimeLabel");
                if (modifiedDateTimeLabel != null)
                {
                    modifiedDateTimeLabel.TextColor = Color.White;
                }
                var contractorInfoLabel = selectedItemGrid.FindByName<Label>("contractorInfoLabel");
                if (contractorInfoLabel != null)
                {
                    contractorInfoLabel.TextColor = Color.White;
                }
            }
        }

        private void ResetTheme()
        {
            if (selectedItemGrid != null)
            {
                selectedItemGrid.BackgroundColor = Color.FromHex("#EAECF5");
                var categoryLabel = selectedItemGrid.FindByName<Label>("categoryLabel");
                if (categoryLabel != null)
                {
                    categoryLabel.TextColor = Color.Default;
                }
                var titleLabel = selectedItemGrid.FindByName<Label>("titleLabel");
                if (titleLabel != null)
                {
                    titleLabel.TextColor = Color.Default;
                }
                var modifiedDateTimeLabel = selectedItemGrid.FindByName<Label>("modifiedDateTimeLabel");
                if (modifiedDateTimeLabel != null)
                {
                    modifiedDateTimeLabel.TextColor = Color.Default;
                }
                var contractorInfoLabel = selectedItemGrid.FindByName<Label>("contractorInfoLabel");
                if (contractorInfoLabel != null)
                {
                    contractorInfoLabel.TextColor = Color.Default;
                }
            }
        }
    }
}