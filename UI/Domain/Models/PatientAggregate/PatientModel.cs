﻿using SharedKernel;
using UI.Areas.PatientCaseReview.Pages.ViewModels.Enums;
using UI.Domain.Models.Enumerations;

namespace UI.Domain.Models.PatientAggregate
{
    public class PatientModel : AggregateRoot
    {
        public static class Factory
        {
            public static PatientModel Create(string firstName, string lastName, string email, string phoneNumber, string gender, int age, int weight, int height)
            {
                var newPatient =
                    new PatientModel(firstName:firstName, lastName:lastName, email:email, phoneNumber:phoneNumber, gender:gender, age:age, weight:weight, height: height) 
                    { TrackingState = TrackableEntities.Common.Core.TrackingState.Added};
                return newPatient;
            }
        }
        private PatientModel(string firstName, string lastName, string email, string phoneNumber, string gender,int age , int weight, int height ) 
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Age = age;
            Weight = weight;
            Height = height;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Gender { get; private set; }
        public int Age { get; private set; }
        public int Weight { get; private set; }
        public int Height { get; private set; }

    }
}
