using System.Linq.Expressions;
using System.Reflection.Metadata;
using MongoDB.Driver;

namespace MongoDB.DriverEx.Fluent;

public class FluentFilterDefinitionBuilder<TDocument>
{

    public List<UpdateDefinition<TDocument>> Steps { get; set; }
    private UpdateDefinitionBuilder<TDocument> builer;
    internal FluentFilterDefinitionBuilder() {
        Steps = new();
        builer = Builders<TDocument>.Update;
    }

    public static FluentFilterDefinitionBuilder<TDocument> New()
    {
        return new FluentFilterDefinitionBuilder<TDocument>();
    }

    public UpdateDefinition<TDocument> Build()
    {
        return builer.Combine(Steps);
    }

    /// <summary>
    /// Creates an add to set operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>An add to set operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> AddToSet<TItem>(FieldDefinition<TDocument> field, TItem value)
    {
        Steps.Add(builer.AddToSet<TItem>(field, value));
        return this;
    }

    /// <summary>
    /// Creates an add to set operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>An add to set operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> AddToSet<TItem>(Expression<Func<TDocument, IEnumerable<TItem>>> field, TItem value)
    {
        return AddToSet<TItem>(new ExpressionFieldDefinition<TDocument>(field), value);
    }

    /// <summary>
    /// Creates an add to set operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="values">The values.</param>
    /// <returns>An add to set operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> AddToSetEach<TItem>(FieldDefinition<TDocument> field, IEnumerable<TItem> values)
    {
        Steps.Add(builer.AddToSetEach<TItem>(field, values));
        return this;
    }

    /// <summary>
    /// Creates an add to set operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="values">The values.</param>
    /// <returns>An add to set operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> AddToSetEach<TItem>(Expression<Func<TDocument, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
    {
        return AddToSetEach(new ExpressionFieldDefinition<TDocument>(field), values);
    }

    /// <summary>
    /// Creates a bitwise and operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A bitwise and operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> BitwiseAnd<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.BitwiseAnd<TField>(field, value));
        return this;
    }

    /// <summary>
    /// Creates a bitwise and operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A bitwise and operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> BitwiseAnd<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return BitwiseAnd(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates a bitwise or operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A bitwise or operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> BitwiseOr<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.BitwiseOr<TField>(field, value));
        return this;
    }

    /// <summary>
    /// Creates a bitwise or operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A bitwise or operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> BitwiseOr<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return BitwiseOr(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates a bitwise xor operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A bitwise xor operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> BitwiseXor<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.BitwiseXor(field, value));
        return this;
    }

    /// <summary>
    /// Creates a bitwise xor operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A bitwise xor operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> BitwiseXor<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return BitwiseXor(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates a current date operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="type">The type.</param>
    /// <returns>A current date operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> CurrentDate(FieldDefinition<TDocument> field, UpdateDefinitionCurrentDateType? type = null)
    {
        Steps.Add(builer.CurrentDate(field, type));
        return this;
    }

    /// <summary>
    /// Creates a current date operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="type">The type.</param>
    /// <returns>A current date operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> CurrentDate(Expression<Func<TDocument, object>> field, UpdateDefinitionCurrentDateType? type = null)
    {
        return CurrentDate(new ExpressionFieldDefinition<TDocument>(field), type);
    }

    /// <summary>
    /// Creates an increment operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>An increment operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Inc<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.Inc<TField>(field, value));
        return this;
    }

    /// <summary>
    /// Creates an increment operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>An increment operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Inc<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return Inc(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates a max operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A max operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Max<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.Max<TField>(field, value));
        return this;
    }

    /// <summary>
    /// Creates a max operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A max operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Max<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return Max(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates a min operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A min operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Min<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.Min<TField>(field, value));
        return this;
    }

    /// <summary>
    /// Creates a min operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A min operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Min<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return Min(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates a multiply operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A multiply operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Mul<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.Mul<TField>(field, value));
        return this;
    }

    /// <summary>
    /// Creates a multiply operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A multiply operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Mul<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return Mul(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates an update pipeline.
    /// </summary>
    /// <param name="pipeline">The pipeline.</param>
    /// <returns>An update pipeline.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Pipeline(PipelineDefinition<TDocument, TDocument> pipeline)
    {
        Steps.Add(builer.Pipeline(pipeline));
        return this;
    }

    /// <summary>
    /// Creates a pop operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>A pop operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PopFirst(FieldDefinition<TDocument> field)
    {
        Steps.Add(builer.PopFirst(field));
        return this;
    }

    /// <summary>
    /// Creates a pop first operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>A pop first operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PopFirst(Expression<Func<TDocument, object>> field)
    {
        return PopFirst(new ExpressionFieldDefinition<TDocument>(field));
    }

    /// <summary>
    /// Creates a pop operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>A pop operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PopLast(FieldDefinition<TDocument> field)
    {
        Steps.Add(builer.PopLast(field));
        return this;
    }

    /// <summary>
    /// Creates a pop first operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>A pop last operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PopLast(Expression<Func<TDocument, object>> field)
    {
        return PopLast(new ExpressionFieldDefinition<TDocument>(field));
    }

    /// <summary>
    /// Creates a pull operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A pull operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Pull<TItem>(FieldDefinition<TDocument> field, TItem value)
    {
        Steps.Add(builer.Pull<TItem>(field, value));
        return this;
    }

    /// <summary>
    /// Creates a pull operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A pull operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Pull<TItem>(Expression<Func<TDocument, IEnumerable<TItem>>> field, TItem value)
    {
        return Pull<TItem>(new ExpressionFieldDefinition<TDocument>(field), value);
    }

    /// <summary>
    /// Creates a pull operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="values">The values.</param>
    /// <returns>A pull operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PullAll<TItem>(FieldDefinition<TDocument> field, IEnumerable<TItem> values)
    {
        Steps.Add(builer.PullAll<TItem>(field, values));
        return this;
    }

    /// <summary>
    /// Creates a pull operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="values">The values.</param>
    /// <returns>A pull operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PullAll<TItem>(Expression<Func<TDocument, IEnumerable<TItem>>> field, IEnumerable<TItem> values)
    {
        return PullAll(new ExpressionFieldDefinition<TDocument>(field), values);
    }

    /// <summary>
    /// Creates a pull operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>A pull operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PullFilter<TItem>(FieldDefinition<TDocument> field, FilterDefinition<TItem> filter)
    {
        Steps.Add(builer.PullFilter<TItem>(field, filter));
        return this;
    }

    /// <summary>
    /// Creates a pull operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>A pull operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PullFilter<TItem>(Expression<Func<TDocument, IEnumerable<TItem>>> field, FilterDefinition<TItem> filter)
    {
        return PullFilter(new ExpressionFieldDefinition<TDocument>(field), filter);
    }

    /// <summary>
    /// Creates a pull operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>A pull operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PullFilter<TItem>(Expression<Func<TDocument, IEnumerable<TItem>>> field, Expression<Func<TItem, bool>> filter)
    {
        return PullFilter(new ExpressionFieldDefinition<TDocument>(field), new ExpressionFilterDefinition<TItem>(filter));
    }

    /// <summary>
    /// Creates a push operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A push operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Push<TItem>(FieldDefinition<TDocument> field, TItem value)
    {
        Steps.Add(builer.Push(field, value));
        return this;
    }

    /// <summary>
    /// Creates a push operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A push operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Push<TItem>(Expression<Func<TDocument, IEnumerable<TItem>>> field, TItem value)
    {
        return Push(new ExpressionFieldDefinition<TDocument>(field), value);
    }

    /// <summary>
    /// Creates a push operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="values">The values.</param>
    /// <param name="slice">The slice.</param>
    /// <param name="position">The position.</param>
    /// <param name="sort">The sort.</param>
    /// <returns>A push operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PushEach<TItem>(FieldDefinition<TDocument> field, IEnumerable<TItem> values, int? slice = null, int? position = null, SortDefinition<TItem> sort = null)
    {
        Steps.Add(builer.PushEach(field, values, slice, position, sort));
        return this;
    }

    /// <summary>
    /// Creates a push operator.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="values">The values.</param>
    /// <param name="slice">The slice.</param>
    /// <param name="position">The position.</param>
    /// <param name="sort">The sort.</param>
    /// <returns>A push operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> PushEach<TItem>(Expression<Func<TDocument, IEnumerable<TItem>>> field, IEnumerable<TItem> values, int? slice = null, int? position = null, SortDefinition<TItem> sort = null)
    {
        return PushEach(new ExpressionFieldDefinition<TDocument>(field), values, slice, position, sort);
    }

    /// <summary>
    /// Creates a field renaming operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="newName">The new name.</param>
    /// <returns>A field rename operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Rename(FieldDefinition<TDocument> field, string newName)
    {
        Steps.Add(builer.Rename(field, newName));
        return this;
    }

    /// <summary>
    /// Creates a field renaming operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="newName">The new name.</param>
    /// <returns>A field rename operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Rename(Expression<Func<TDocument, object>> field, string newName)
    {
        return Rename(new ExpressionFieldDefinition<TDocument>(field), newName);
    }

    /// <summary>
    /// Creates a set operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A set operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Set<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.Set<TField>(field, value));
        return this;
    }

    /// <summary>
    /// Creates a set operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A set operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Set<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return Set(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates a set on insert operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A set on insert operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> SetOnInsert<TField>(FieldDefinition<TDocument, TField> field, TField value)
    {
        Steps.Add(builer.SetOnInsert<TField>(field, value));
        return this;
    }

    /// <summary>
    /// Creates a set on insert operator.
    /// </summary>
    /// <typeparam name="TField">The type of the field.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <returns>A set on insert operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> SetOnInsert<TField>(Expression<Func<TDocument, TField>> field, TField value)
    {
        return SetOnInsert(new ExpressionFieldDefinition<TDocument, TField>(field), value);
    }

    /// <summary>
    /// Creates an unset operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>An unset operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Unset(FieldDefinition<TDocument> field)
    {
        Steps.Add(builer.Unset(field));
        return this;
    }

    /// <summary>
    /// Creates an unset operator.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <returns>An unset operator.</returns>
    public FluentFilterDefinitionBuilder<TDocument> Unset(Expression<Func<TDocument, object>> field)
    {
        return Unset(new ExpressionFieldDefinition<TDocument>(field));
    }

}
