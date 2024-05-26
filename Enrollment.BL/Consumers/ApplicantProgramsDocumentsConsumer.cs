using MassTransit;
using Shared.DTO;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace Enrollment.BL.Consumers
{
    public class ApplicantProgramsDocumentsConsumer : IConsumer<GetApplicantProgramsDocuments>
    {
        private readonly IEnrollmentRequestsService _enrollmentService;
        public ApplicantProgramsDocumentsConsumer(IEnrollmentRequestsService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        public async Task Consume(ConsumeContext<GetApplicantProgramsDocuments> context)
        {
            ApplicantProgramsDocuments admissionPrograms = new ApplicantProgramsDocuments();
            admissionPrograms.ProgramsDocumentsIds = await _enrollmentService.GetApplicantProgramsId(context.Message.ApplicantId);
            if (admissionPrograms.ProgramsDocumentsIds.Count > 0)
                admissionPrograms.isEmpty = false;
            else
                admissionPrograms.isEmpty = true;


            await context.RespondAsync(admissionPrograms);
        }
    }
}