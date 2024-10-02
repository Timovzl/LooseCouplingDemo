using LooseCouplingDemo.Application;
using LooseCouplingDemo.Common.Events;
using LooseCouplingDemo.Domain;
using LooseCouplingDemo.Infrastructure;

namespace LooseCouplingDemo;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();

		builder.Services.AddSingleton<IEventBus, DemoEventBus>();
		builder.Services.AddSingleton(typeof(EventPublisher<>));

		builder.Services.AddSingleton<IngestPersonaliaChangeCommandHandler>();
		builder.Services.AddSingleton<AnnouncePersonaliaChangesPolicy>();
		builder.Services.AddSingleton<IEventHandler<PersonaliaChangedEvent>, AnnouncePersonaliaChangesPolicy>();
		builder.Services.AddSingleton<TrackPersonCountPolicy>();
		builder.Services.AddSingleton<IEventHandler<PersonaliaChangedEvent>, TrackPersonCountPolicy>();
		builder.Services.AddSingleton<IModifyPersonCountCommandHandler, DummyModifyPersonCountCommandHandler>();

		var app = builder.Build();

		app.MapControllers();

		app.Run();
	}
}
