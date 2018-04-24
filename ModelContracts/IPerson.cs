namespace Model
{
    public interface IPerson
    {
        string City { get; set; }
        string Name { get; set; }
        int PostalCode { get; set; }
        string Street { get; set; }

        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}