using Bogus;
using System.Linq;
using Nis.Core.Models;

namespace Nis.Core.Factories
{
    public class PatientFactory : BaseFactory<Patient>
    {
        protected override Faker<Patient> Rules => Faker.Rules((faker, patient) =>
        {
            patient.FirstName = faker.Name.FirstName();
            patient.LastName = faker.Name.LastName();
        });

        public override Patient[] Create(short count = 1) => Enumerable
            .Range(0, count)
            .Select(_ => Rules.Generate())
            .ToArray();
    }
}
