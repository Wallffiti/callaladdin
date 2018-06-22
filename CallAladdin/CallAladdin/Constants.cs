using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin
{
    public class Constants
    {
        // GENERAL

        public const string PRINCIPAL = "PRINCIPAL";
        public const string ADMIN_NUMBER = "+601235678";
        public const int ALLOWABLE_JOB_REQUESTS_PER_MONTH = 33333;  //DEBUG: please change it before go live

        // EVENT ARGS
        public const string USER_PROFILE_UPDATE = "USER_PROFILE_UPDATE";
        public const string REQUESTOR = "REQUESTOR";
        public const string CONTRACTOR = "CONTRACTOR";
        public const string TAB_SWITCH = "TAB_SWITCH";
        public const string HOME = "HOME";
        public const string DASHBOARD = "DASHBOARD";
        public const string USER_PROFILE = "USER_PROFILE";

        //HOME ICONS

        //Row 1
        public const string CONSTRUCTION_TILING_PAINTING = "CONSTRUCTION, TILING & PAINTING";
        public const string WIRING_LIGHING = "WIRING & LIGHTING";
        public const string INTERIOR_DESIGN_CARPENTERS = "INTERIOR DESIGN & CARPENTERS";

        //Row 2
        public const string LAMINATED_TIMBER_FLOORING = "LAMINATED, TIMBER FLOORING";
        public const string CURTAIN_CARPET_WALLPAPER = "CURTAIN, CARPET & WALLPAPER";
        public const string SIGNBOARD = "SIGNBOARD";

        //Row 3
        public const string AIR_CONDITIONER = "AIR CONDITIONER";
        public const string PLASTERED_CEILING = "PLASTERED CEILING";
        public const string ALUMINIUM_GLASSWORKS = "ALUMINIUM & GLASSWORKS";

        //Row 4
        public const string ALARM_CCTV = "ALARM & CCTV";
        public const string CLEANING_SERVICES = "CLEANING SERVICES";
        public const string LANDSCAPING_POND = "LANDSCAPING & POND";

        //Row 5
        public const string GATES_STEEL_WORKS = "GATES & STEEL WORKS";
        public const string PLUMBER = "PLUMBER";
        public const string PEST_CONTROL = "PEST CONTROL";

        //Row 6
        public const string FENGSHUI_CONSULTATION = "FENGSHUI CONSULTATION";
        public const string GENERAL_WORKERS = "GENERAL WORKERS";
        public const string HOUSE_MOVERS = "HOUSE MOVERS";
    }
}
