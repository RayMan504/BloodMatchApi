public class BloodMatchService
{
    private static readonly Dictionary<BloodType, List<string>> Compat = new()
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

    // check if the donor blood type is compatible with the recipient blood type
    public bool IsMatch(BloodType donor, BloodType recipient)
        => Compat.ContainsKey(recipient) && Compat[recipient].Contains(donor.ToString());

    public List<string> GetBloodTypeMatch(Role role, BloodType bloodType)
    {
        // if user is recipient
        if(role == Role.Recipient)
        {
            // constant time acesss to compatible blood types
            return Compat[bloodType];
        }
        // if user is donor
        else
        {
            // linear time access to compatible blood types by iterating through dictionary and checking if recipient blood type is in the list of compatible blood types for each donor blood type
            return Compat.Where(kvp => kvp.Value.Contains(bloodType.ToString())).Select(kvp => kvp.Key.ToString()).ToList();
        }
    }

    public IReadOnlyList<string> GetSupportedBloodTypes()
        => Enum.GetValues<BloodType>().Select(bt => bt.ToString()).ToList();
}