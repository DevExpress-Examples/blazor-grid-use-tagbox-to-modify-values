<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/516731409/25.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1104359)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Blazor Grid - Use TagBox as a Column Editor

This example uses our Blazor [TagBox](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxTagBox-2) Editor to modify column values (within the DevExpress Blazor Grid UI component). As you can see, our TagBox Editor allows users to select multiple values when editing cell values or specify multiple search criteria within our Grid’s Filter Row.

![image](image.png)

## Implementation Details

To add our TagBox component to your Grid, you must specify appropriate data cell and/or Filter Row templates.

### Cell Editor Template

To display the DevExpress Blazor TagBox within edited cells, you must:

1. Activate data editing in the DevExpress Blazor Grid component ([EditRow](https://docs.devexpress.com/Blazor/404758/components/grid/editing-and-validation/edit-modes/edit-row) or [EditCell](https://docs.devexpress.com/Blazor/404756/components/grid/editing-and-validation/edit-modes/edit-cell) mode).
2. Place a [DxTagBox](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxTagBox-2) editor into the [DxGridDataColumn.CellEditTemplate](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGridDataColumn.CellEditTemplate).
3. Handle the editor's [ValuesChanged](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxTagBox-2.ValuesChanged) event and assign selected values to `context.EditModel`.

```razor
<DxGridDataColumn FieldName="Privileges" Caption="System Privileges">
    <CellEditTemplate Context="editContext">
        @{
            var user = editContext.EditModel as User;
            <DxTagBox Data="@AvailablePrivileges"
                      TData="string"
                      TValue="string"
                      Values="@(user!.Privileges)"
                      ValuesExpression="@(() => user.Privileges)"
                      ValuesChanged="@((newValues) => OnPrivilegesChanged(user, newValues))"
                      NullText="Assign privileges..." />
        }
    </CellEditTemplate>
</DxGridDataColumn>
```
```cs
private void OnPrivilegesChanged(User user, IEnumerable<string> newValues) {
    user.Privileges = newValues.ToList();
}
```

### Filter Row Template

To display the DevExpress Blazor TagBox component in a Filter Row cell, you must:

1. Activate the [ShowFilterRow](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGrid.ShowFilterRow) property to display the integrated DevExpress Grid [Filter Row](https://docs.devexpress.com/Blazor/404325/components/grid/data-shaping/filter-data/filter-row).
2. Place a [DxTagBox](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxTagBox-2) editor into the [DxGridDataColumn.FilterRowCellTemplate](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxGridDataColumn.FilterRowCellTemplate).
3. Handle the editor's [ValuesChanged](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxTagBox-2.ValuesChanged) event and set `context.FilterCriteria` to custom filter criteria (based on selected values).

```razor
<DxGridDataColumn FieldName="Privileges" Caption="System Privileges" >
    <FilterRowCellTemplate>
        @{
            var items = TagBoxFilterRowUtils.GetValueByFunctionOperator(context.FilterCriteria, nameof(User.Privileges));
        }
        <DxTagBox TData="string"
                  TValue="string"
                  Data="AvailablePrivileges"
                  Values="items"
                  ValuesChanged="(newValues) => { context.FilterCriteria = TagBoxFilterRowUtils.CreateFilterCriteriaByValues(newValues, nameof(User.Privileges)); }" />
    </FilterRowCellTemplate>
</DxGridDataColumn>
```

```cs
public class TagBoxFilterRowUtils {
    public static IEnumerable<string> GetValueByFunctionOperator(CriteriaOperator criteria, string fieldName) {
        var aggregateOperand = criteria as AggregateOperand;
        if (aggregateOperand.ReferenceEqualsNull() || aggregateOperand.AggregateType != Aggregate.Exists)
            return null;
        if (aggregateOperand.CollectionProperty is not OperandProperty operandProperty || operandProperty.PropertyName != fieldName)
            return null;
        if (aggregateOperand.Condition is not InOperator inOperator)
            return null;
        return inOperator.Operands.OfType<OperandValue>().Select(r => r.Value?.ToString());
    }

    public static CriteriaOperator CreateFilterCriteriaByValues(IEnumerable<string> values, string fieldName) {
        if (values.Count() == 0)
            return null;
        return new AggregateOperand(fieldName, Aggregate.Exists, new InOperator("", values));
    }
}
```

## Files to Review

* [Index.razor](./CS/DxBlazorApplication1/Pages/Index.razor)
* [TagBoxFilterRowUtils.cs](./CS/DxBlazorApplication1/TagBoxFilterRowUtils.cs)
* [DataService.cs](./CS/DxBlazorApplication1/Data/DataService.cs)
* [User.cs](./CS/DxBlazorApplication1/Data/User.cs)

## Documentation

* [Editing and Validation in Blazor Grid](https://docs.devexpress.com/Blazor/403454/components/grid/editing-and-validation)
* [Filter Data in Blazor Grid](https://docs.devexpress.com/Blazor/404326/components/grid/data-shaping/filter-data/filter-data)

<!-- feedback -->
## Does This Example Address Your Development Requirements/Objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-grid-use-tagbox-to-modify-values&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-grid-use-tagbox-to-modify-values&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
