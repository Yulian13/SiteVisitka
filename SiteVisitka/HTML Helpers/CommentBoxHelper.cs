using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SiteVisitka.HTML_Helpers
{
    public static class CommentBoxHelper
    {
        /*
         <div class="row comment">
			<div class="container">
				<div class="row">
					<div class="col-md-6 Name">@contextName</div>
					<div class="col-md-6 Adress">@contextAddress</div>
				</div>
				<div class="row">
					<div class="col-md-12 TextComment">
						<p>@contextText</p>
					</div>
				</div>
			</div>
		</div>
        */

        public static HtmlString CreateCommentBox(this IHtmlHelper html, IMyTagCreator tagName, IMyTagCreator tagAddress, IMyTagCreator tagText, params IMyTagCreator[] tagOthers)
        {
            var writer = new System.IO.StringWriter();

            TagBuilder divName = CreatTagDiv("col-md-6 Name", tagName);
            TagBuilder divAddress = CreatTagDiv("col-md-6 Adress", tagAddress);
            TagBuilder divText = CreatTagDiv("col-md-12 TextComment", tagText);
            foreach(IMyTagCreator myTag in tagOthers)
                divText.InnerHtml.AppendHtml(myTag.CreateTag());

            TagBuilder divRowField = CreatTagDiv("row RowCommentField", divName, divAddress);
            TagBuilder divRowText = CreatTagDiv("row", divText);

            TagBuilder divContainer = CreatTagDiv("container", divRowField, divRowText);

            TagBuilder divMainRow = CreatTagDiv("row comment", divContainer);

            divMainRow.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString().Replace("&lt;", "<").Replace("&gt;", ">"));
        }

        private static TagBuilder CreatTagDiv(string CssClases, IMyTagCreator CreatorTag)
        {
            TagBuilder CreatedDiv = new("div");
            CreatedDiv.AddCssClass(CssClases);
            CreatedDiv.InnerHtml.AppendHtml(CreatorTag?.CreateTag());

            return CreatedDiv;
        }

        private static TagBuilder CreatTagDiv(string CssClases, params TagBuilder[] tagContexts)
        {
            TagBuilder CreatedTag = new("div");
            CreatedTag.AddCssClass(CssClases);

            foreach (TagBuilder tag in tagContexts)
            {
                CreatedTag.InnerHtml.AppendHtml(tag);
            }

            return CreatedTag;
        }

    }
    public interface IMyTagCreator
    {
        TagBuilder CreateTag();
    }

    abstract public class TagFields
    {
        protected readonly string _name;
        protected readonly string _placeholder;
        protected readonly string _cssClass;
        protected readonly string _type;
        protected readonly string _context;
        protected readonly string _id;
        protected readonly string _onClick;
        protected readonly string _style;

        public TagFields(string name = "", string placeholder = "", string cssClass = "", string type = ""
            , string context = "", string id = "", string onClick = "", string style ="")
        {
            _name = name;
            _placeholder = placeholder;
            _cssClass = cssClass;
            _type = type;
            _context = context;
            _id = id;
            _onClick = onClick;
            _style = style;
        }
    }

    public class MyCreatorTagP : TagFields, IMyTagCreator
    {
        public MyCreatorTagP(string cssClass = "", string type = "", string context = "", string id = "", string onClick = "", string style = "")
            : base(null, null, cssClass, type, context, id, onClick, style){}

        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("p");
            tag.InnerHtml.Append(_context);
            return tag;
        }
    }

    public class MyCreatorTagInput : TagFields, IMyTagCreator
    {
        public MyCreatorTagInput(string name = "", string placeholder = "", string cssClass = "", string type = "",string id = "", string onClick = "", string style = "")
            : base(name, placeholder, cssClass, type, null, id, onClick, style) { }

        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("input");
            tag.MergeAttribute("type",_type);
            tag.MergeAttribute("name", _name);
            tag.MergeAttribute("placeholder", _placeholder);
            tag.AddCssClass(_cssClass);
            return tag;
        }
    }

    public class MyCreatorTagTextarea : TagFields, IMyTagCreator
    {
        public MyCreatorTagTextarea(string name = "", string placeholder = "", string cssClass = "", string type = "", string context = "", string id = "", string onClick = "", string style = "")
    : base(name, placeholder, cssClass, type, context, id, onClick, style) { }

        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("textarea");
            tag.AddCssClass(_cssClass);
            tag.MergeAttribute("name", _name);
            tag.MergeAttribute("style", _style);
            tag.MergeAttribute("placeholder", _placeholder);
            tag.InnerHtml.Append(_context);
            return tag;
        }
    }

    public class MyCreatorTagButton : TagFields, IMyTagCreator
    {
        public MyCreatorTagButton(string name = "", string cssClass = "", string type = "", string context = "", string id = "", string onClick = "", string style = "")
    : base(name, null, cssClass, type, context, id, onClick, style) { }

        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("button");
            tag.AddCssClass(_cssClass);
            tag.MergeAttribute("type", _type);
            tag.MergeAttribute("id", _id);
            tag.MergeAttribute("onClick", _onClick);
            tag.InnerHtml.Append(_context);
            return tag;
        }
    }

    public class MyCreatorTagDiv : TagFields, IMyTagCreator
    {
        public MyCreatorTagDiv(string name = "",string cssClass = "", string type = "", string context = "", string id = "", string onClick = "", string style = "")
    : base(name, null, cssClass, type, context, id, onClick, style) { }

        public TagBuilder CreateTag()
        {
            TagBuilder tag = new("div");
            tag.AddCssClass(_cssClass);
            tag.MergeAttribute("id", _id);
            tag.MergeAttribute("style", _style);
            tag.InnerHtml.Append(_context);
            return tag;
        }
    }

}
