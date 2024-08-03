using Google.Cloud.Firestore;
using Schedule.Bot.Domain.Database.Connection;

namespace Schedule.Bot.Firestore.Connection;

public class FirestoreConnector : DbConnector<FirestoreAccess, FirestoreDb>
{
    public FirestoreConnector(FirestoreAccessProvider accessProvider) : base(accessProvider)
    {
    }

    public override FirestoreDb Connect()
    {
        FirestoreAccess access = AccessProvider.Provide();

        FirestoreDbBuilder firestoreDbBuilder = new()
        {
            ProjectId = access.ProjectId,
            JsonCredentials = access.ToString()
        };

        return firestoreDbBuilder.Build();
    }
}
