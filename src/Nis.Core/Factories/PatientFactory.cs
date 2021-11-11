using Bogus;
using System.Linq;
using Bogus.DataSets;
using Nis.Core.Models;

namespace Nis.Core.Factories
{
    public class PatientFactory : BaseFactory<Patient>
    {
        protected override Faker<Patient> Rules => Faker.Rules((faker, patient) =>
        {
            var gender = faker.PickRandom<Name.Gender>();

            patient.FirstName = faker.Name.FirstName(gender);
            patient.LastName = faker.Name.LastName(gender);
        });

        public override Patient[] Create(short count = 1) => Enumerable
            .Range(0, count)
            .Select(_ => Rules.Generate())
            .ToArray();
    }
}
