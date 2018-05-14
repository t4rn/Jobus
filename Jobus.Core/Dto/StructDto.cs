namespace Jobus.Core.Dto
{
    public struct StructDto<V, D>
    {
        public V Value { get; set; }
        public D Description { get; set; }
    }
}
