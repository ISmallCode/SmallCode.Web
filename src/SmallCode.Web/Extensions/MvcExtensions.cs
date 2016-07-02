using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using SmallCode.Web.DataModels;
using SmallCode.Web.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace SmallCode.Web.Extensions
{
    public static class MvcExtensions
    {
        /// <summary>
        /// 枚举转下拉强类型视图
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlContent EnumToDropDownListFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var modelExplorer =
            ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
            var htmlAttributes2 = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var selectList = new List<SelectListItem>();
            Type type = typeof(TModel);

            PropertyInfo propInfo = type.GetProperty(modelExplorer.Metadata.PropertyName);
            if (propInfo != null)
            {
                Attribute objAttr = propInfo.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();
                if (objAttr != null)
                {
                    DisplayAttribute attr = objAttr as DisplayAttribute;
                    if (attr != null)
                    {
                        var option = new SelectListItem() { Value = "" };
                        option.Text = attr.Name;
                        selectList.Add(option);
                    }
                }
            }


            var enumType = modelExplorer.ModelType;
            foreach (var value in Enum.GetValues(enumType))
            {
                var field = enumType.GetField(value.ToString());
                var option = new SelectListItem() { Value = value.ToString() };
                var display = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                option.Text = display != null ? display.Name : value.ToString();
                option.Selected = Equals(value, modelExplorer.Model);
                selectList.Add(option);
            }
            return html.DropDownList(modelExplorer.Metadata.PropertyName, selectList, htmlAttributes2);
        }

        /// <summary>
        /// 枚举转下拉
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="eEnum"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IHtmlContent EnumToDropDownList(this IHtmlHelper helper, Enum eEnum, string name)
        {
            var selectList = new List<SelectListItem>();
            var enumType = eEnum.GetType();
            foreach (var value in Enum.GetValues(enumType))
            {
                var field = enumType.GetField(value.ToString());
                var option = new SelectListItem() { Value = value.ToString() };
                var display = field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                option.Text = display != null ? display.Name : value.ToString();
                option.Selected = Equals(value, eEnum);
                selectList.Add(option);
            }
            return helper.DropDownList(name, selectList);
        }

        /// <summary>
        /// 图片展示扩展
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlContent ImageFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var modelExplorer =
                ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
            // var propertyname = ExpressionHelper.GetExpressionText(expression);
            var htmlAttributes2 = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            return ImageHelper(html, modelExplorer, htmlAttributes2);

        }

        public static IHtmlContent ImageFor<TModel, TProperty>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            return ImageFor(html, expression, null);
        }


        internal static IHtmlContent ImageHelper(IHtmlHelper html, ModelExplorer modelExplorer, IDictionary<string, object> htmlAttributes = null)
        {
            //属性值
            var value = modelExplorer.Model.ToString();

            if (string.IsNullOrEmpty(value))
            {
                return HtmlString.Empty;
            }
            var img = new TagBuilder("img");
            img.Attributes.Add("src", value);
            //属性名
            img.Attributes.Add("id", modelExplorer.Metadata.PropertyName);
            img.MergeAttributes(htmlAttributes, true);

            var tagA = new TagBuilder("a");
            tagA.MergeAttribute("href", value);
            tagA.MergeAttribute("target", "_blank");
            tagA.InnerHtml.AppendHtml(img.ToString());
            return tagA;
        }
    }


}


