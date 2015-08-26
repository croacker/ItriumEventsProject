namespace ItriumData.data
{
    public class CredentialHolder
    {
        public int ID { get; set; }
        public string name { get; set; }

        public override string ToString() {
            return name;
        }
    }
}
