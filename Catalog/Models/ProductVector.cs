namespace Catalog.Models;

public class ProductVector
{
    [VectorStoreRecordKey]
    public int Id { get; set; }
    [VectorStoreRecordData]
    public string Name { get; set; } = null!;
    [VectorStoreRecordData]
    public string Description { get; set; } = null!;
    [VectorStoreRecordData]
    public decimal Price { get; set; }
    [VectorStoreRecordData]
    public string ImageUrl { get; set; } = null!;

    [NotMapped]
    [VectorStoreRecordVector(384, DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Vector { get; set; }
}