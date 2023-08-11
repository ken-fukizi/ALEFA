﻿using Microsoft.Extensions.Options;
using SharedKernel;
using UI.Areas.PatientCaseReview.Pages.ViewModels;
using UI.Data;
using UI.Domain.Models.DemographicsAggregate;
using UI.Domain.Models.Enumerations;
using UI.Domain.Models.PatientAggregate;
using UI.Domain.Models.SymptomsAggregate;

namespace UI.Services
{
    public class PatientDetailsService : IPatientDetailsService
    {
        private readonly ApplicationDbContext _dbContext;
        public PatientDetailsService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void SaveDemographics(DemographicsViewModel demographicsViewModel)
        {
            var demographics = DemographicsModel.Factory.Create
                (
                    patientGuid: demographicsViewModel.PatientGuid,
                        currentTown: demographicsViewModel.CurrentTown,
                        previousTowns: demographicsViewModel.PreviousTowns,
                        occupation: demographicsViewModel.Occupation
                );
            demographics.EntityIdentifier = demographicsViewModel.PatientGuid;
            demographics.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;
            _dbContext.SaveChanges();
        }

        public Guid SavePatientDetails(PatientViewModel patientViewModel)
        {
            var patient = PatientModel.Factory.Create
                (
                    firstName: patientViewModel.FirstName, 
                    lastName:patientViewModel.LastName, 
                    email: patientViewModel.Email, 
                    phoneNumber: patientViewModel.PhoneNumber, 
                    //gender: Enumeration.FromDisplayName<EnumGender>(patientDetails.Gender.ToString()),
                    gender: patientViewModel.Gender,
                    age: patientViewModel.Age,
                    weight:patientViewModel.Weight,
                    height: patientViewModel.Height
                );
            patient.EntityIdentifier = Guid.NewGuid();
            patient.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;
            //Bypassing the usage of the repository pattern for now.

            //ToDo: Check if it exists first
            _dbContext.Patients.Add( patient );
            _dbContext.SaveChanges();
            return patient.EntityIdentifier;
            
        }

        public void SavePatientSymptoms(SymptomsViewModel symptomsViewModel)
        {
            var symptoms = 
                SymptomsModel.Factory.Create
                (
                    patientGuid: symptomsViewModel.PatientGuid, 
                    options: symptomsViewModel.Options
                );
            
        }
    }
}
