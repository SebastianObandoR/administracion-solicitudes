
function ModulePanel({ title, children }) {
  return (
    <section className="crud-panel">
      <div className="crud-panel-header">
        <h2>{title}</h2>
      </div>

      <div className="crud-panel-body">
        {children}
      </div>
    </section>
  );
}

export default ModulePanel;
