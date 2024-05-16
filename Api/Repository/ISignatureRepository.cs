using Core;

namespace Api.Repository
{
    public interface ISignatureRepository
    {
    void CreateSignature(Signature signature);
    List<Signature> GetAllSignatures();
    Signature GetSignatureById(string id);
    void UpdateSignature(Signature signature);
    void DeleteSignature(string id);
    }
}

