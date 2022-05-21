using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalPropertyAttribuite : PropertyAttribute
{
    public ActionOnConditionTrue Action { get; private set; }
    public ConditionOperator Operator { get; private set; }
    public string[] Conditions { get; private set; }

    public ConditionalPropertyAttribuite(ActionOnConditionTrue action, ConditionOperator conditionOperator, params string[] conditions)
    {
        Action = action;
        Operator = conditionOperator;
        Conditions = conditions;
    }
}
public enum ConditionOperator
{
    And,
    Or,
}
public enum ActionOnConditionTrue
{   
    DontDraw,   
    Disable,
}