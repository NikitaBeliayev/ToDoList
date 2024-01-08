using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Documents.RequestDto;
using Domain.Documents;
using Domain.Users;
using Shared;

namespace Application.Documents.PostDocument;

public class PostDocumentCommandHandler : ICommandHandler<PostDocumentCommand, PostDocumentDto>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PostDocumentCommandHandler(IDocumentRepository documentRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _documentRepository = documentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<PostDocumentDto>> Handle(PostDocumentCommand request, CancellationToken cancellationToken)
    {
        Stream stream = request.FileStream;
        byte[] bytes;

        User? user = await _userRepository.GetUserByExpressionAsync(
            user => user.EmailAddress == EmailAddress.BuildEmail(request.DocumentDto.EmailAddress).Value, cancellationToken);
        if (user is null)
        {
            return null; //change it to error
        }
        
        using (var binaryReader = new BinaryReader(stream))
        {
            bytes = binaryReader.ReadBytes((int)stream.Length);
        }

        Document document = new Document()
        {
            Id = Guid.NewGuid(),
            Title = new Title()
            {
                Value = request.FileName
            },
            AccessStatus = request.DocumentDto.AccessStatus,
            OwnerId = user.Id,
            Users = {user}
            
        };
        await _documentRepository.PostDocument(document);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<PostDocumentDto>.Success(request.DocumentDto);
    }
}