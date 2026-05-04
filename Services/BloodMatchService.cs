public class BloodMatchService
{
    private static readonly Dictionary<string, List<string>> Compat = new()
    {
        ["O-"] = new() { "O-" },
        ["O+"] = new() { "O-", "O+" },
        ["A-"] = new() { "O-", "A-" },
        ["A+"] = new() { "O-", "O+", "A-", "A+" },
        ["B-"] = new() { "O-", "B-" },
        ["B+"] = new() { "O-", "O+", "B-", "B+" },
        ["AB-"] = new() { "O-", "A-", "B-", "AB-" },
        ["AB+"] = new() { "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+" }
    };

    // check if the donor blood type is compatible with the recipient blood type
    public bool IsMatch(string donor, string recipient)
        => Compat.ContainsKey(recipient) && Compat[recipient].Contains(donor);
}