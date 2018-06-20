using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class JobRequestViewModel : BaseViewModel
    {
        private string contractorIcon;
        private string jobRequestType;
        private string jobRequestImage;
        private string title;
        private string scopeOfWork;
        private string selectedStartDate;
        private string selectedStartTime;
        private string selectedCity;
        private IList<string> cities;

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
            set { jobRequestImage = value; OnPropertyChanged("JobRequestImage"); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }

        public string ScopeOfWork
        {
            get { return scopeOfWork; }
            set { scopeOfWork = value; OnPropertyChanged("ScopeOfWork"); }
        }

        public string SelectedStartDate
        {
            get { return selectedStartDate; }
            set { selectedStartDate = value; OnPropertyChanged("SelectedStartDate"); }
        }

        public string SelectedStartTime
        {
            get { return selectedStartTime; }
            set { selectedStartTime = value; OnPropertyChanged("SelectedStartTime"); }
        }

        public string SelectedCity
        {
            get { return selectedCity; }
            set { selectedCity = value; OnPropertyChanged("SelectedCity"); }
        }

        public IList<string> Cities
        {
            get { return cities; }
            set { cities = value; OnPropertyChanged("Cities"); }
        }


        public JobRequestViewModel()
        {
            ContractorIcon = "CallAladdin.Assets.Images.Contractors.AIR_CONDITIONER.png";    //TEST
            JobRequestType = "AIR CONDITIONER"; //TEST
            JobRequestImage = "CallAladdin.Assets.Images.default_avatar_image.jpeg";  //TEST
        }
    }
}
