using CallAladdin.Model;
using CallAladdin.Services;
using CallAladdin.Services.Interfaces;
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

        private string userSystemUUID;
        private IJobService jobService;

        public JobRequestViewModel(object owner)
        {
            var parameters = (JobRequestParameters)owner;
            if (parameters != null)
            {
                ContractorIcon = GetIconByCategory(parameters.JobCategoryType);
                JobRequestType = parameters.JobCategoryType;
                userSystemUUID = parameters.UserSystemUUID;
            }
            jobService = new JobService();
            JobRequestImage = "CallAladdin.Assets.Images.default_avatar_image.jpeg";
        }

        private string GetIconByCategory(string category)
        {
            var path = "CallAladdin.Assets.Images.default_avatar_image.jpeg";

            switch (category)
            {
                case Constants.AIR_CONDITIONER:
                    path = "CallAladdin.Assets.Images.Contractors.AIR_CONDITIONER.png";
                    break;
                case Constants.ALARM_CCTV:
                    path = "CallAladdin.Assets.Images.Contractors.ALARM_N_CCTV.png";
                    break;
                case Constants.ALUMINIUM_GLASSWORKS:
                    path = "CallAladdin.Assets.Images.Contractors.ALUMINIUM_N_GLASS_WORKS.png";
                    break;
                case Constants.CLEANING_SERVICES:
                    path = "CallAladdin.Assets.Images.Contractors.CLEANING_SERVICES.png";
                    break;
                case Constants.CONSTRUCTION_TILING_PAINTING:
                    path = "CallAladdin.Assets.Images.Contractors.CONSTRUCTION_TILING_N_PAINTING.png";
                    break;
                case Constants.CURTAIN_CARPET_WALLPAPER:
                    path = "CallAladdin.Assets.Images.Contractors.CURTAIN_CARPET_N_WALLPAPER.png";
                    break;
                case Constants.FENGSHUI_CONSULTATION:
                    path = "CallAladdin.Assets.Images.Contractors.FENGSHUI_CONSULTATION.png";
                    break;
                case Constants.GATES_STEEL_WORKS:
                    path = "CallAladdin.Assets.Images.Contractors.GATES_N_STEEL_WORKS.png";
                    break;
                case Constants.GENERAL_WORKERS:
                    path = "CallAladdin.Assets.Images.Contractors.GENERAL_WORKERS.png";
                    break;
                case Constants.HOUSE_MOVERS:
                    path = "CallAladdin.Assets.Images.Contractors.HOUSE_MOVERS.png";
                    break;
                case Constants.INTERIOR_DESIGN_CARPENTERS:
                    path = "CallAladdin.Assets.Images.Contractors.INTERIOR_DESIGN_N_CARPENTERS.png";
                    break;
                case Constants.LAMINATED_TIMBER_FLOORING:
                    path = "CallAladdin.Assets.Images.Contractors.LAMINATED_TIMBER_FLOORING.png";
                    break;
                case Constants.LANDSCAPING_POND:
                    path = "CallAladdin.Assets.Images.Contractors.LANDSCAPING_N_POND.png";
                    break;
                case Constants.PEST_CONTROL:
                    path = "CallAladdin.Assets.Images.Contractors.PEST_CONTROL.png";
                    break;
                case Constants.PLASTERED_CEILING:
                    path = "CallAladdin.Assets.Images.Contractors.PLASTERED_CEILING.png";
                    break;
                case Constants.PLUMBER:
                    path = "CallAladdin.Assets.Images.Contractors.PLUMBER.png";
                    break;
                case Constants.SIGNBOARD:
                    path = "CallAladdin.Assets.Images.Contractors.SIGNBOARD.png";
                    break;
                case Constants.WIRING_LIGHING:
                    path = "CallAladdin.Assets.Images.Contractors.WIRING_N_LIGHTING.png";
                    break;
            }

            return path;
        }
    }
}
