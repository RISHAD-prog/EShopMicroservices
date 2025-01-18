using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{

    public interface IQuery<out Tresponse>: IRequest<Tresponse> where Tresponse : notnull
    {
    }
}
