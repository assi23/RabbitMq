﻿using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
	public class BaseRepository<M> where M : Base
	{
		public void Create(M model) {
			using (var context = new AnimesContext()) {
				context.Set<M>().Add(model);
				context.SaveChanges();
			}
		}
		public List<M> Read() {
			List<M> list = new List<M>();
			using (var context = new AnimesContext()) {
				list = context.Set<M>().ToList();
			}
			return list;
		}
		public M Read(int id) {
			M model = Activator.CreateInstance<M>();
			using (var context = new AnimesContext()) {
				model = context.Set<M>().Find(id);
			}
			return model;
		}
		public void Update(M model) {
			using (var context = new AnimesContext()) {
				context.Entry<M>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				context.SaveChanges();
			}
		}
		public void Delete(int id) {
			using (var context = new AnimesContext()) {
				context.Entry<M>(this.Read(id)).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
				context.SaveChanges();
			}
		}
	}
}
