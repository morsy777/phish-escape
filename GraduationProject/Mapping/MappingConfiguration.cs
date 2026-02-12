namespace GraduationProject.Mapping;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email);

        config.NewConfig<ApplicationUser, UserProfileResponse>()
            .Map(dest => dest.Username, src => src.Email);

    }
}
