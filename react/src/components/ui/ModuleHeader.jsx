
function ModuleHeader({ title, children }) {
  return (
    <section className="crud-header">
      {title && <h1>{title}</h1>}

      <div className="crud-header-actions">
        {children}
      </div>
    </section>
  );
}

export default ModuleHeader;
