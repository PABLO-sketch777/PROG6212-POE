using Xunit;
using System.IO; namespace RaceDay.Api.Tests; public class EndpointPlanTests{[Fact] public void PlanningDocumentContainsRequiredAreas(){var p=File.ReadAllText(Path.Combine("..","..","..","..","..","docs","endpoint-plan.md"));foreach(var x in new[]{"auth/register","auth/login","profile","events","categories","enrollments","result"})Assert.Contains(x,p);}}
