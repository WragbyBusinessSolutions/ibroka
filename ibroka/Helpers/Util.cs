using ibroka.Data;
using ibroka.Models.LeadManagement;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ibroka.Helpers
{
    public class Util
    {
        public static int GetDaysBetweenCurretAndGivenDate(DateTime dt)
        {
            DateTime today = DateTime.Now;
            return (today - dt).Days;

        }

        public static int CalculateAge(DateTime? dateOfBirth)
        {
            DateTime dt = dateOfBirth ?? DateTime.Now;
            int age = 0;
            age = DateTime.Now.Year - dt.Year;
            if (DateTime.Now.DayOfYear < dt.DayOfYear)
                age = age - 1;

            return age;
        }

        public static string getAgents(IFormCollection formCollection)
        {
            List<Agent> agents = new List<Agent>();

            string agent1 = formCollection["Agent1"].ToString();
            string agent2 = formCollection["Agent2"].ToString();
            string agent3 = formCollection["Agent3"].ToString();
            string agent4 = formCollection["Agent4"].ToString();
            string agent5 = formCollection["Agent5"].ToString();

            string prop1 = formCollection["prop1"].ToString();
            string prop2 = formCollection["prop2"].ToString();
            string prop3 = formCollection["prop3"].ToString();
            string prop4 = formCollection["prop4"].ToString();
            string prop5 = formCollection["prop5"].ToString();

            var agent = new Agent();
            if (!string.IsNullOrEmpty(agent1))
            {
                agent = new Agent();
                agent.AgentName = agent1;
                if (!string.IsNullOrEmpty(prop1))
                {
                    agent.Proportion = prop1;
                }
                else
                {
                    agent.Proportion = "";
                }
                agents.Add(agent);
            }

            if (!string.IsNullOrEmpty(agent2))
            {
                agent = new Agent();
                agent.AgentName = agent2;
                if (!string.IsNullOrEmpty(prop2))
                {
                    agent.Proportion = prop2;
                }
                else
                {
                    agent.Proportion = "";
                }
                agents.Add(agent);
            }

            if (!string.IsNullOrEmpty(agent3))
            {
                agent = new Agent();
                agent.AgentName = agent3;
                if (!string.IsNullOrEmpty(prop3))
                {
                    agent.Proportion = prop3;
                }
                else
                {
                    agent.Proportion = "";
                }
                agents.Add(agent);
            }

            if (!string.IsNullOrEmpty(agent4))
            {
                agent = new Agent();
                agent.AgentName = agent4;
                if (!string.IsNullOrEmpty(prop4))
                {
                    agent.Proportion = prop4;
                }
                else
                {
                    agent.Proportion = "";
                }
                agents.Add(agent);
            }

            if (!string.IsNullOrEmpty(agent5))
            {
                agent = new Agent();
                agent.AgentName = agent5;
                if (!string.IsNullOrEmpty(prop5))
                {
                    agent.Proportion = prop5;
                }
                else
                {
                    agent.Proportion = "";
                }
                agents.Add(agent);
            }
            string agentStr = "";
            if (agents.Count > 0)
            {
                agentStr = JsonConvert.SerializeObject(agents);
            }

            return agentStr;
        }

        public static List<Agent> getAgentListFromJson(string agents)
        {

            List<Agent> agentList = new List<Agent>();
            if (!string.IsNullOrEmpty(agents))
            {
                var array = JArray.Parse(agents);
                foreach (var item in array)
                {
                    var agent = new Agent();
                    agent.AgentName = item["AgentName"].ToString();
                    agent.Proportion = item["Proportion"].ToString();
                    agentList.Add(agent);
                }
            }
            return agentList;
        }

        public static List<Client> GetClientsAgeInYrs(List<Client> clients)
        {
            foreach (var client in clients)
            {

                client.Age = CalculateAge(client.DateOfBirth);
                client.DaysCreated = GetDaysBetweenCurretAndGivenDate(client.DateCreated);

            }

            return clients;
        }

        public static string GetPolicyName(List<PolicyClassMaster> policyMaster, Guid policyId)
        {

            return policyMaster.Where(p => p.Id == policyId).FirstOrDefault().Name;
        }

        public static string GetInsurerName(ApplicationDbContext context, Guid id)
        {

            return context.GlobalInsurers.Where(i => i.Id == id).FirstOrDefault().Name;
        }

        public static string GetImageUrl(string imgUrl)
        {
            if(!string.IsNullOrEmpty(imgUrl))
            {
              return  imgUrl ="/"+ imgUrl.Replace("\\", "/");
            }
            return "";
        }

    }
}
