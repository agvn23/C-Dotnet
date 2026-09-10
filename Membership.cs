//Base class
public class Membership
{
    public string MemberName { get; set; }
    public virtual string GetBenefits()
    {
        return "Standard access to facility.";
    }
}
//Derived class
public class StandardMembership : Membership
{
    public override string GetBenefits()
    {
        return "Access to gym equipment during standard hours.";
    }
}
//Derived class
public class PremiumMembership : Membership
{
    public override string GetBenefits()
    {
        return "24/7 access, free towel service and guest passes.";
    }
}
//Lifetime membership - sealed override stops anyone else from overriding this further
public class LifetimeMembership : Membership
{
    public sealed override string GetBenefits()
    {
        return "All premium benefits plus lifetime VIP lounge access.";
    }
}