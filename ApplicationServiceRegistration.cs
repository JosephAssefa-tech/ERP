using System;

public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{

		services.AddTransient<IApplicantRepository, ApplicantRepository>();
		services.AddTransient<IApplicantService, ApplicantService>();
		return services;
	}
}
