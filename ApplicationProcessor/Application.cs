using System;
using System.Text;
using ULaw.ApplicationProcessor.Enums;  

namespace ULaw.ApplicationProcessor
{
    public class Application
    {
        public Application(string faculty, string CourseCode, DateTime Startdate, string Title, string FirstName, string LastName, DateTime DateOfBirth, bool requiresVisa)
        {
            this.ApplicationId = new Guid();
            this.Faculty = faculty;
            this.CourseCode = CourseCode;
            this.StartDate = Startdate;
            this.Title = Title;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.RequiresVisa = RequiresVisa;
            this.DateOfBirth = DateOfBirth;
        }

        public Guid ApplicationId { get; private set; }
        public string Faculty { get; private set; }
        public string CourseCode { get; private set; }
        public DateTime StartDate { get; private set; }
        public string Title { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public bool RequiresVisa { get; private set; }

        public DegreeGradeEnum DegreeGrade { get; set; }
        public DegreeSubjectEnum DegreeSubject { get; set; }

        // This could be passed into constructor too?
        public string EmailAddress => "AdmissionsTeam@Ulaw.co.uk";
        public string CourseRefText => string.Format("course reference: {0} starting on {1}", this.CourseCode, this.StartDate.ToLongDateString());

        public string Process()
        {
            // Start
            var result = new StringBuilder("<html><body><h1>Your Recent Application from the University of Law</h1>");

            // Adding introduction
            result.Append(string.Format("<p> Dear {0}, </p>", FirstName));
            result.Append("<p/> Further to your recent application");

            // Selecting body to add
            if (DegreeGrade == DegreeGradeEnum.twoTwo)
            {
                AddTwoTwo(result);
            }
            else if (DegreeGrade == DegreeGradeEnum.third)
            {
                AddThird(result);
            }
            else if (DegreeSubject == DegreeSubjectEnum.law || DegreeSubject == DegreeSubjectEnum.lawAndBusiness)
            {
                AddLaw(result);
            }
            else
            {
                AddGeneric(result);
            }

            // Adding signature
            result.Append(" Yours sincerely,");
            result.Append("<p/> The Admissions Team,");

            // End
            result.Append(string.Format("</body></html>"));

            return result.ToString();
        }

        private void AddTwoTwo(StringBuilder result)
        {
            result.Append(string.Format(" for our {0}, we are writing to inform you that we are currently assessing your information and will be in touch shortly.", CourseRefText));
            result.Append(string.Format("<br/> If you wish to discuss any aspect of your application, please contact us at {0}.<br/>", EmailAddress));
        }

        private void AddThird(StringBuilder result)
        {
            result.Append(", we are sorry to inform you that you have not been successful on this occasion.");
            result.Append(string.Format("<br/> If you wish to discuss the decision further, or discuss the possibility of applying for an alternative course with us, please contact us at {0}.<br>", EmailAddress));
        }

        private void AddLaw(StringBuilder result)
        {
            decimal depositAmount = 350.00M;

            result.Append(string.Format(", we are delighted to offer you a place on our {0}.", CourseRefText));
            result.Append(string.Format("<br/> This offer will be subject to evidence of your qualifying {0} degree at grade: {1}.", DegreeSubject.ToDescription(), DegreeGrade.ToDescription()));
            result.Append(string.Format("<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £{0} deposit fee to secure your place.", depositAmount.ToString()));
            result.Append(string.Format("<br/> We look forward to welcoming you to the University,<br/>"));
        }

        private void AddGeneric(StringBuilder result)
        {
            result.Append(string.Format(" for our {0}, we are writing to inform you that we are currently assessing your information and will be in touch shortly.", CourseRefText));
            result.Append(string.Format("<br/> If you wish to discuss any aspect of your application, please contact us at {0}.<br/>", EmailAddress));
        }
    }
}

