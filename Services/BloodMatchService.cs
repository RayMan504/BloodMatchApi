public class BloodMatchService
{
    private static readonly Dictionary<BloodType, List<string>> Compat = new()
    {
        [BloodType.ON] = new() { "O-" },
        [BloodType.OP] = new() { "O-", "O+" },
        [BloodType.AN] = new() { "O-", "A-" },
        [BloodType.AP] = new() { "O-", "O+", "A-", "A+" },
        [BloodType.BN] = new() { "O-", "B-" },
        [BloodType.BP] = new() { "O-", "O+", "B-", "B+" },
        [BloodType.ABN] = new() { "O-", "A-", "B-", "AB-" },
        [BloodType.ABP] = new() { "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+" }
    };

    // check if the donor blood type is compatible with the recipient blood type
    public bool IsMatch(string donor, BloodType recipient)
        => Compat.ContainsKey(recipient) && Compat[recipient].Contains(donor);

    public List<string> GetBloodTypeMatch(Role role, BloodType bloodType)
    {
        // if user is recipient
            // constant time acesss to compatible blood types
        // if user is donor
            // linear time access to compatible blood types by iterating through dictionary and checking if donor blood type is in the list of compatible blood types for each recipient blood type
    }

    public IReadOnlyList<string> GetSupportedBloodTypes()
        => Enum.GetNames<BloodType>();
}