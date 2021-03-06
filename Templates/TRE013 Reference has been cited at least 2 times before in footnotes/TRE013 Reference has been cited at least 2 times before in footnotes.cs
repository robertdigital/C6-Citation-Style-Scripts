//TRE013
//Version 1.0
//Reference has been cited at least 2 times before in footnotes

using System;
using System.Linq;
using System.Collections.Generic;
using SwissAcademic.Citavi;
using SwissAcademic.Citavi.Metadata;
using SwissAcademic.Collections;

namespace SwissAcademic.Citavi.Citations
{
	public class CustomTemplateCondition
		:
		ITemplateConditionMacro
	{
		//Reference has been cited at least 2 times before in footnotes
		
		public bool IsTemplateForReference(ConditionalTemplate template, Citation citation)
		{
			if (citation == null) return false;
			if (citation.Reference == null) return false;
			
			FootnoteCitation thisFootnoteCitation = citation as FootnoteCitation;
			if (thisFootnoteCitation == null) return false;
			
			CitationManager citationManager = citation.CitationManager;
			if (citationManager == null) return false;
			if (citationManager.FootnoteCitations == null || citationManager.FootnoteCitations.Count <= 2) return false; //no 3rd mention possible	
			if (thisFootnoteCitation.IsUniqueFootnote) return false; 		//there is just one
			if (!thisFootnoteCitation.IsRepeatingCitation) return false; 	//is has not mentioned before, therefore in cannot be the 3rd (or higher) mention
			
			//still here? we iterate over all footnote citations
			int previousMentions = 0;
			foreach(FootnoteCitation otherFootnoteCitation in citationManager.FootnoteCitations)
			{
				if (otherFootnoteCitation == null) continue;
				if (otherFootnoteCitation.Reference == null) continue;
				if (otherFootnoteCitation.Equals(thisFootnoteCitation)) break; //we reached the current footnote citation
				
				if (otherFootnoteCitation.Reference.Equals(thisFootnoteCitation.Reference))
				{
					previousMentions++; //found another previous mention
				}
				
				if (previousMentions >=2) return true;
			}
			
			return previousMentions >= 2;
		}
	}
}
