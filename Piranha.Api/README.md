#Piranha.API
This is the new simplified application API for Piranha CMS that will be used both
by applications as well as the manager interface.

It will most likely be moved into the core repository when finished and replace the
current models.

##Usage

The API object is a disposable object that controls the unit of work for the underlying
repositories, just like a DbContext. The Api is instanciated with:

	using (var api = new Piranha.Api()) {
		...

		// Saves that changes made in the repositories
		api.SaveChanges() ;
	}

###CategoryRepository

	IList<ApiModels.Category> Get(Expression<Func<Entities.Category, bool>> predicate = null) ;
	ApiModels.Category GetById(Guid id) ;
	ApiModels.Category GetByPermalink(string permalink) ;
	void Add(ApiModels.Category model) ;
	void Update(ApiModels.Category model) ;
	void Remove(ApiModels.Category model) ;

###ParamRepository

	IList<ApiModels.Param<string>> Get(Expression<Func<Entities.Param, bool>> predicate = null) ;
	ApiModels.Param<T> GetById<T>(Guid id) ;
	ApiModels.Param<T> GetByName<T>(string name) ;
	void Add<T>(ApiModels.Param<T> model) ;
	void Update<T>(ApiModels.Param<T> model) ;
	void Ensure(string name, object value) ;
	void Remove<T>(ApiModels.Param<T> model) ;

The library is distributed with the Gnu LGPL license. For more information, 
please refer to LICENSE.md.