using Core.Bases;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Featuers.Vacation.Command.Models
{
    public class UpdateVacation : IRequest<Response<string>>
    {
        public required string ApproverEmail { get; set; }
        [Required]
        public int VacationId { get; set; }
        [Required]
        public VacationRequestStatus Status { get; set; }
    }
}
