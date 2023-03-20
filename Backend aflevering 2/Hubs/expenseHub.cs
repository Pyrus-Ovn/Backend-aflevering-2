using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Backend_aflevering_2.Controllers;


namespace Backend_aflevering_2.Hubs
{
    [HubName("expensehub")]
    public class ExpenseHub : Hub
    {
        public ExpenseHub()
        {

        }
    }
}



