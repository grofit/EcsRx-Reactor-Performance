using System;
using System.Collections.Generic;
using EcsRx;
using EcsRx.Collections;
using EcsRx.Components;
using EcsRx.Components.Database;
using EcsRx.Entities;
using EcsRx.Events;
using EcsRx.Executor;
using EcsRx.Executor.Handlers;
using EcsRx.Groups.Observable;
using EcsRx.Systems.Handlers;

namespace EcsRxPerformanceTests
{
	public class EventSystem : IEventSystem
	{
		public IMessageBroker MessageBroker { get; }

		public EventSystem(IMessageBroker messageBroker)
		{ MessageBroker = messageBroker; }

		public void Publish<T>(T message)
		{ MessageBroker.Publish(message); }

		public IObservable<T> Receive<T>()
		{ return MessageBroker.Receive<T>(); }
	}
	
    public abstract class EcsRxApplication
	{
		public ISystemExecutor SystemExecutor { get; }
		public IEventSystem EventSystem { get; }
		public IEntityCollectionManager EntityCollectionManager { get; }
	
		protected EcsRxApplication()
		{
			// For sending events around
			EventSystem = new EventSystem(new MessageBroker());
			
			// For mapping component types to underlying indexes
			var componentTypeAssigner = new DefaultComponentTypeAssigner();
			var allComponents = componentTypeAssigner.GenerateComponentLookups();
			
			var componentLookup = new ComponentTypeLookup(allComponents);
			// For interacting with the component databases
			var componentDatabase = new ComponentDatabase(componentLookup);
			var componentRepository = new ComponentRepository(componentLookup, componentDatabase);	
			
			// For creating entities, collections, observable groups and managing Ids
			var entityFactory = new DefaultEntityFactory(new IdPool(), componentRepository);
			var entityCollectionFactory = new DefaultEntityCollectionFactory(entityFactory);
			var observableGroupFactory = new DefaultObservableObservableGroupFactory();
			EntityCollectionManager = new EntityCollectionManager(entityCollectionFactory, observableGroupFactory, componentLookup);
	
			// All system handlers for the system types you want to support
			var reactsToEntityHandler = new ReactToEntitySystemHandler(EntityCollectionManager);
			var reactsToGroupHandler = new ReactToGroupSystemHandler(EntityCollectionManager);
			var reactsToDataHandler = new ReactToDataSystemHandler(EntityCollectionManager);
			var manualSystemHandler = new ManualSystemHandler(EntityCollectionManager);
			var setupHandler = new SetupSystemHandler(EntityCollectionManager);
			var teardownHandler = new TeardownSystemHandler(EntityCollectionManager);
	
			var conventionalSystems = new List<IConventionalSystemHandler>
			{
				setupHandler,
				teardownHandler,
				reactsToEntityHandler,
				reactsToGroupHandler,
				reactsToDataHandler,
				manualSystemHandler
			};
			
			// The main executor which manages how systems are given information
			SystemExecutor = new SystemExecutor(conventionalSystems);
		}
	
		public void StartApplication()
		{
			ApplicationStarting();
			ApplicationStarted();
		}

		protected virtual void ApplicationStarting() { }
		protected abstract void ApplicationStarted();    
	}
}