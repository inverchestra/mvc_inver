using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Models;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers;
using System.ComponentModel.DataAnnotations;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Filters
    {
        public bool Gestination { get; set; }
        public bool Hypertension { get; set; }
        public bool Latepregnancy { get; set; }
        public bool Earlypregnancy { get; set; }
        public bool Thyroid { get; set; }
        public bool Diabetes { get; set; }
        public bool Twins { get; set; }
        public bool Tripplets { get; set; }
        public bool Asthma { get; set; }

        public bool Outdoorsports { get; set; }
        public bool Swimming { get; set; }
        public bool Yoga { get; set; }
        public bool Adventuresports { get; set; }
        public bool Gardening { get; set; }
        public bool Animalcare { get; set; }
        public bool Pets { get; set; }
        public bool Arts { get; set; }
        public bool Modeling { get; set; }
        public bool Interiordesigning { get; set; }
        public bool Travelling { get; set; }


        private string _fid;

        public string Id
        {
            get { return _fid; }
            set { _fid = value; }
        }

        private string _filterText;

        public string Filtertext
        {
            get { return _filterText; }
            set { _filterText = value; }
        }
        private bool _privacy;

        public bool Privacy
        {
            get { return _privacy; }
            set { _privacy = value; }
        }
        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _interest;

        public string Interests
        {
            get { return _interest; }
            set { _interest = value; }
        }
    }
}