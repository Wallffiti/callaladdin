using CallAladdin.Helper;
using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.ViewModel
{
    public class EditJobViewModel : BaseViewModel
    {
        private const string CHOOSE_PHOTO_FROM_CAMERA = "Choose photo from camera";
        private const string BROWSE_PHOTO_FROM_FOLDER = "Browse photo from folder";

        private BaseViewModel parentViewModel;
        //private Job selectedJob;
        private string contractorIcon;
        private string jobRequestType;
        private string jobRequestImage;
        private string selectedPhotoOption;
        private IList<string> photoOptionSelections;
        private Job jobRequest;

        public string ContractorIcon
        {
            get { return contractorIcon; }
            set { contractorIcon = value; OnPropertyChanged("ContractorIcon"); }
        }

        public string JobRequestType
        {
            get { return jobRequestType; }
            set { jobRequestType = value; OnPropertyChanged("JobRequestType"); }
        }

        public string JobRequestImage
        {
            get { return jobRequestImage; }
            set { jobRequestImage = value; UpdateJobRequest(); OnPropertyChanged("JobRequestImage"); }
        }

        public IList<string> PhotoOptionSelections
        {
            get { return photoOptionSelections; }
            set { photoOptionSelections = value; OnPropertyChanged("PhotoOptionSelections"); }
        }

        public string SelectedPhotoOption
        {
            get { return selectedPhotoOption; }
            set { selectedPhotoOption = value; OnPropertyChanged("SelectedPhotoOption"); }
        }

        public Job JobRequest
        {
            get { return jobRequest; }
            set { jobRequest = value; OnPropertyChanged("JobRequest"); }
        }

        public ICommand ChangeProfileImageCmd { get; set; }

        public EditJobViewModel(object owner)
        {
            Job selectedJob = null;

            if (owner is DashboardUserControlViewModel)
            {
                parentViewModel = (DashboardUserControlViewModel)owner;
                selectedJob = ((DashboardUserControlViewModel)parentViewModel).GetSelectedJob();
            }
            else if (owner is JobViewViewModel)
            {
                parentViewModel = (JobViewViewModel)owner;
                selectedJob = ((JobViewViewModel)parentViewModel).GetSelectedJob();
            }

            if (selectedJob != null)
            {
                ContractorIcon = Helper.Utilities.GetIconByCategory(selectedJob.Category);
                JobRequestType = selectedJob.Category;
                jobRequestImage = selectedJob.ImagePath;
                //TODO
            }

            LoadImageUploaderOptions();

            ChangeProfileImageCmd = new Xamarin.Forms.Command(e =>
            {
                ChangeProfileImageAsync();
            }, (param) =>
            {
                return true;
            });
        }

        private async void ChangeProfileImageAsync()
        {
            string filePath = "";

            try
            {
                if (selectedPhotoOption == CHOOSE_PHOTO_FROM_CAMERA)
                {
                    filePath = await Utilities.TakePhoto(Guid.NewGuid().ToString().Replace("-", ""));
                }
                else if (selectedPhotoOption == BROWSE_PHOTO_FROM_FOLDER)
                {
                    filePath = await Utilities.PickPhoto();
                }
            }
            catch (Exception ex)
            {

            }

            JobRequestImage = filePath;
        }

        private void LoadImageUploaderOptions()
        {
            PhotoOptionSelections = new List<string>
            {
                CHOOSE_PHOTO_FROM_CAMERA,
                BROWSE_PHOTO_FROM_FOLDER
            };
            SelectedPhotoOption = BROWSE_PHOTO_FROM_FOLDER;
        }

        private void UpdateJobRequest()
        {
            JobRequest = new Job
            {
                //TODO
            };
        }
    }
}
