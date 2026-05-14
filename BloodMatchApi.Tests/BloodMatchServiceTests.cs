using System.Linq;
using Xunit;
using System;

public class BloodMatchServiceTests
{
    [Fact]
    public void IsMatch_CoversAllCompatibilityPairs()
    {
        var service = new BloodMatchService();

        var compat = new System.Collections.Generic.Dictionary<BloodType, System.Collections.Generic.List<string>>()
        {
            [BloodType.ON] = new() { BloodType.ON.ToString() },
            [BloodType.OP] = new() { BloodType.ON.ToString(), BloodType.OP.ToString() },
            [BloodType.AN] = new() { BloodType.ON.ToString(), BloodType.AN.ToString() },
            [BloodType.AP] = new() { BloodType.ON.ToString(), BloodType.OP.ToString(), BloodType.AN.ToString(), BloodType.AP.ToString() },
            [BloodType.BN] = new() { BloodType.ON.ToString(), BloodType.BN.ToString() },
            [BloodType.BP] = new() { BloodType.ON.ToString(), BloodType.OP.ToString(), BloodType.BN.ToString(), BloodType.BP.ToString() },
            [BloodType.ABN] = new() { BloodType.ON.ToString(), BloodType.AN.ToString(), BloodType.BN.ToString(), BloodType.ABN.ToString() },
            [BloodType.ABP] = new() { BloodType.ON.ToString(), BloodType.OP.ToString(), BloodType.AN.ToString(), BloodType.AP.ToString(), BloodType.BN.ToString(), BloodType.BP.ToString(), BloodType.ABN.ToString(), BloodType.ABP.ToString() }
        };

        // recipients: enum values
        var recipients = compat.Keys;

        // donors: all unique donor strings used in the compatibility lists
        var donors = new System.Collections.Generic.HashSet<string>(compat.Values.SelectMany(list => list));

        foreach (var recipient in recipients)
        {
            foreach (var donor in donors)
            {
                var expected = compat.ContainsKey(recipient) && compat[recipient].Contains(donor);
                var actual = service.IsMatch(Enum.Parse<BloodType>(donor), recipient);
                Assert.Equal(expected, actual);
            }
        }
    }

    [Fact]
    public void GetCompatibleDonors_ReturnsExpectedLists()
    {
        var service = new BloodMatchService();

        var compat = new System.Collections.Generic.Dictionary<BloodType, System.Collections.Generic.List<string>>()
        {
            [BloodType.ON] = new() { BloodType.ON.ToString() },
            [BloodType.OP] = new() { BloodType.ON.ToString(), BloodType.OP.ToString() },
            [BloodType.AN] = new() { BloodType.ON.ToString(), BloodType.AN.ToString() },
            [BloodType.AP] = new() { BloodType.ON.ToString(), BloodType.OP.ToString(), BloodType.AN.ToString(), BloodType.AP.ToString() },
            [BloodType.BN] = new() { BloodType.ON.ToString(), BloodType.BN.ToString() },
            [BloodType.BP] = new() { BloodType.ON.ToString(), BloodType.OP.ToString(), BloodType.BN.ToString(), BloodType.BP.ToString() },
            [BloodType.ABN] = new() { BloodType.ON.ToString(), BloodType.AN.ToString(), BloodType.BN.ToString(), BloodType.ABN.ToString() },
            [BloodType.ABP] = new() { BloodType.ON.ToString(), BloodType.OP.ToString(), BloodType.AN.ToString(), BloodType.AP.ToString(), BloodType.BN.ToString(), BloodType.BP.ToString(), BloodType.ABN.ToString(), BloodType.ABP.ToString() }
        };

        foreach (var bloodType in compat.Keys)
        {
            var expected = compat[bloodType].OrderBy(x => x).ToList();
            var actual = service.GetBloodTypeMatch(Role.Recipient, bloodType).OrderBy(x => x).ToList();
            Assert.Equal(expected, actual);
        }
    }

    [Fact]
    public void GetSupportedBloodTypes_ReturnsAllEnumStringValues()
    {
        var service = new BloodMatchService();
        var expected = Enum.GetValues<BloodType>().Select(bt => bt.ToString()).ToList();

        var actual = service.GetSupportedBloodTypes().Select(bt => bt.ToString()).ToList();

        Assert.Equal(expected, actual);
    }

}
