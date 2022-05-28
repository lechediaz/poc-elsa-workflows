namespace FactoryApp.Models
{
    public class RequestDetail
    {
        #region Fields
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int RawMaterialId { get; set; }
        public double Quantity { get; set; }
        #endregion

        #region Relations
        public virtual RawMaterial RawMaterial { get; set; }
        public virtual Request Request { get; set; }
        #endregion
    }
}
