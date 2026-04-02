function getDefaultDates() {
    const now = new Date();

    const start = new Date(now.getFullYear(), now.getMonth(), 1);
    const end = new Date(now.getFullYear(), now.getMonth() + 1, 0);

    return {
        dataInicio: start.toISOString().split("T")[0],
        dataFim: end.toISOString().split("T")[0],
    }
}

export function useDashboardFilters() {
    return {
        defaultValues: getDefaultDates()
    }
}