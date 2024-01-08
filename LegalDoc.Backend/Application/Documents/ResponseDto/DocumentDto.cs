namespace Application.Documents.ResponseDto;

public class DocumentDto
{
    public string FileName { get; set; }
    public string OwnerEmailAddress { get; set; }
    public byte[] Bytes { get; set; }
}