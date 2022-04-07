using EpsmGest.Data;
using EpsmGest.ViewModel.Requisition;
using EPSMGest.Models.Space;
using Microsoft.EntityFrameworkCore;

namespace EpsmGest.Services.Space
{
	public class SpaceService : ISpaceService
	{
		public readonly EpsmGestDbContext AppDb;

		public SpaceService(EpsmGestDbContext Db)
		{
			AppDb = Db;
		}

		/*
			Requesition Space 
		 */
		public List<RequestSpaceModel> GetRequestSpaces()
		{
			return AppDb.RequestSpace.ToList();
		}

		public RequestSpaceModel? GetRequestSpace(int id)
		{
			return AppDb.RequestSpace.FirstOrDefault(x => x.Id == id);
		}

		public void CreateRequestSpace(CreateReqSpaceViewModel model)
		{
			AppDb.RequestSpace.Add(model.ReqSpace);
			AppDb.SaveChanges();
		}

		public bool EditRequestSpace(RequestSpaceModel model)
		{
			var reqSpace = AppDb.RequestSpace.Include(x => x.Requisition).FirstOrDefault(x => x.Id == model.Id);
			if (reqSpace == null)
				return false;
			reqSpace.SpaceId = model.SpaceId;
			reqSpace.Start = model.Start;
			reqSpace.End = model.End;
			AppDb.RequestSpace.Update(reqSpace);			
			reqSpace.Requisition.Description = model.Requisition.Description;
			AppDb.Requisition.Update(reqSpace.Requisition);
			AppDb.SaveChanges();
			return true;
		}

		public bool ChangeAprovel(int id)
		{
			var reqSpace = AppDb.RequestSpace.Include(x => x.Requisition).FirstOrDefault(x => x.Id == id && x.End > DateTime.Now);
			if (reqSpace == null)
				return false;
			if (!reqSpace.Approval)
				reqSpace.Approval = true;
			else
				reqSpace.Approval = false;
			AppDb.SaveChanges();
			return true;
		}

		public bool RemoveRequest(int id)
		{
			var reqSpace = AppDb.RequestSpace.Include(x => x.Requisition).FirstOrDefault(x => x.Id == id);
			if (reqSpace == null)
				return false;
			reqSpace.Requisition.Delivered = 1;
			AppDb.Requisition.Update(reqSpace.Requisition);
			AppDb.SaveChanges();
			AppDb.RequestSpace.Remove(reqSpace);
			return true;
		}

		/*
			Space
		 */

		public List<SpaceModel> GetSpaces()
		{
			return AppDb.Space.ToList();
		}

		public SpaceModel? GetSpace(int id)
		{
			return AppDb.Space.FirstOrDefault(x => x.Id == id);
		}

		public bool CreateSpace(SpaceModel model)
		{
			if (AppDb.Space.Where(x => x.Name == model.Name).Any())
				return false;
			AppDb.Space.Add(model);
			AppDb.SaveChanges();
			return true;
		}

		public bool EditSpace(SpaceModel model)
		{
			var space = AppDb.Space.FirstOrDefault(x => x.Id == model.Id);
			if (space == null)
				return false;
			space.Name = model.Name;
			AppDb.SaveChanges();
			return true;
		}

		public bool RemoveSpace(int id)
		{
			var space = AppDb.Space.FirstOrDefault(x => x.Id == id);
			var reqSpace = AppDb.RequestSpace.Where(x => x.SpaceId == id);
			if (space == null)
				return false;
			AppDb.Space.Remove(space);
			return true;
		}
	}
}
