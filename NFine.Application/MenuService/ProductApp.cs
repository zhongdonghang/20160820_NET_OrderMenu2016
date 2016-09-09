using NFine.Code;
using NFine.Domain._02_ViewModel;
using NFine.Domain._03_Entity.MenuBiz;
using NFine.Domain._04_IRepository.MenuBiz;
using NFine.Repository.MenuBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.MenuService
{
  public  class ProductApp
    {
        private IT_PRODUCTRepository service = new T_PRODUCTRepository();
        private IT_PRODUCT_CATEORYRepository objCategoryservice = new T_PRODUCT_CATEORYRepository();
        private ISYS_FILESRepository fileService = new SYS_FILESRepository();
        private IRS_PRODUCT_FILERepository fileRsProductService = new RS_PRODUCT_FILERepository();

        public void SettingImageForProduct(SYS_FILESEntity fileEntity,string productOID)
        {

          //  fileEntity.FileSize = 0;
            fileEntity.ReadCount = 0;
            fileEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId;
            fileEntity.Category = 0;
            fileEntity.DeletionStateCode = 0;
            fileEntity.Enabled = 0;
            fileEntity.SortCode = 0;
            fileEntity.Description = "--";
            fileEntity.CreateOn = DateTime.Now;
            fileEntity.CreateUserId = 0;
            fileEntity.CreateBy = OperatorProvider.Provider.GetCurrent().UserName;
            fileEntity.ModifiedOn = DateTime.Now;
            fileEntity.ModifiedUserId = 0;
            fileEntity.ModifiedBy = OperatorProvider.Provider.GetCurrent().UserName;
            if (fileService.Insert(fileEntity) > 0)
            {
                int fileoid = fileService.IQueryable().Max(t => t.OID);
                List<RS_PRODUCT_FILEEntity> rsList = fileRsProductService.FindList("select * from RS_PRODUCT_FILE where ProductID=" + productOID + "");
                if (rsList.Count > 0)
                {
                    foreach (var item in rsList)
                    {
                        item.FileID = fileoid;
                        fileRsProductService.Update(item);
                    }
                }
                else
                {
                    RS_PRODUCT_FILEEntity rsEntity = new RS_PRODUCT_FILEEntity();
                    rsEntity.FileID = fileoid;
                    rsEntity.ProductID = int.Parse(productOID);
                    rsEntity.OrgID = OperatorProvider.Provider.GetCurrent().OrgId; ;
                    fileRsProductService.Insert(rsEntity);
                }
            }
        }

        private FullProductViewModel Entity2ViewModel(T_PRODUCTEntity entity,string _filePath)
        {
            FullProductViewModel vm = new FullProductViewModel();
            vm.OID = entity.OID;
            vm.Code = entity.Code;
            vm.CName = entity.CName;
            vm.Price1 = entity.Price1;
            vm.Price2 = entity.Price2;
            vm.Price3 = entity.Price3;
            vm.PriceString = entity.PriceString;
            vm.PCategory = entity.PCategory;
            vm.PContent = entity.PContent;
            vm.OrgID = entity.OrgID;
            vm.DeletionStateCode = entity.DeletionStateCode;
            vm.Enabled = entity.Enabled;
            vm.SortCode = entity.SortCode;
            vm.Description = entity.Description;
            vm.CreateOn = entity.CreateOn;
            vm.CreateUserId = entity.CreateUserId;
            vm.CreateBy = entity.CreateBy;
            vm.ModifiedOn = entity.ModifiedOn;
            vm.ModifiedUserId = entity.ModifiedUserId;
            vm.ModifiedBy = entity.ModifiedBy;
            vm.filePath = _filePath;
            return vm;
        }

        /// <summary>
        /// 分页按查询出类别列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<T_PRODUCTEntity> GetList(Pagination pagination, string keyword, int OrgId)
        {
           // List<FullProductViewModel> vmList = new List<FullProductViewModel>();

            var expression = ExtLinq.True<T_PRODUCTEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.CName.Contains(keyword));
            }

            expression = expression.And(t => t.OrgID == OrgId);
            List<T_PRODUCTEntity> list = service.FindList(expression, pagination);

            //foreach (var item in list)
            //{
            //    //int fileId = fileRsProductService.FindEntity(t => t.ProductID == item.OID).FileID;
            //    //string FilePath = fileService.FindEntity(t => t.OID == fileId).FilePath;
            //    vmList.Add(Entity2ViewModel(item, ""));
            //}
            return list;
        }

        public FullProductViewModel GetForm(string keyValue)
        {
            T_PRODUCTEntity obj = service.FindEntity(int.Parse(keyValue));

            RS_PRODUCT_FILEEntity rsEntity = fileRsProductService.FindEntity(t => t.ProductID == obj.OID);
            string FilePath = "";
            if (rsEntity != null)
            {
                int fileId = fileRsProductService.FindEntity(t => t.ProductID == obj.OID).FileID;
                FilePath = fileService.FindEntity(t => t.OID == fileId).FileName;
            }

            return Entity2ViewModel(obj, FilePath);
        }

        public void DeleteForm(string keyValue) 
        {
            int deleteOID = int.Parse(keyValue);
            service.Delete(t => t.OID == deleteOID);
        }

        public void SubmitForm(T_PRODUCTEntity objT_PRODUCTEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))//编辑
            {
                T_PRODUCTEntity oldT_PRODUCTEntity = service.FindEntity(int.Parse(keyValue));
                oldT_PRODUCTEntity.CName = objT_PRODUCTEntity.CName;
                oldT_PRODUCTEntity.PCategory = objT_PRODUCTEntity.PCategory;
                oldT_PRODUCTEntity.Price1 = objT_PRODUCTEntity.Price1;
                oldT_PRODUCTEntity.Price2 = objT_PRODUCTEntity.Price2;
                oldT_PRODUCTEntity.SortCode = objT_PRODUCTEntity.SortCode;
                oldT_PRODUCTEntity.PContent = objT_PRODUCTEntity.PContent;
                service.Update(oldT_PRODUCTEntity);
            }
            else
            {
                objT_PRODUCTEntity.Code = Common.CreateNo();
                objT_PRODUCTEntity.PriceString = "--";
                objT_PRODUCTEntity.DeletionStateCode = 0;
                objT_PRODUCTEntity.Enabled = 0;
                objT_PRODUCTEntity.CreateOn = DateTime.Now;
                objT_PRODUCTEntity.ModifiedOn = DateTime.Now;
                objT_PRODUCTEntity.CreateBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_PRODUCTEntity.ModifiedBy = OperatorProvider.Provider.GetCurrent().UserName;
                objT_PRODUCTEntity.CreateUserId = 0;
                objT_PRODUCTEntity.ModifiedUserId = 0;
                objT_PRODUCTEntity.Description = objCategoryservice.FindEntity(objT_PRODUCTEntity.PCategory).CName;
                service.Insert(objT_PRODUCTEntity);
            }
        }
    }
}
