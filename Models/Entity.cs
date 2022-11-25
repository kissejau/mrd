public record Entity
{

    private DateTime _createdDate { get; set; } = DateTime.Now;
    public DateTime CreatedDate => _createdDate; // GENIUS

    private string _id { get; set; } = Guid.NewGuid().ToString();
    public string Id => _id; // GENIUS

    public void SetId(string id)
    {
        this._id = id;
    }

    public void SetDateTime(DateTime dateTime)
    {
        this._createdDate = dateTime;
    }

}