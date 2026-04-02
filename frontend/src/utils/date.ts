export function formatDateBR(dateString: string): string {
    const [year, month, day] = dateString.split("-").map(Number);
    return new Date(year, month - 1, day).toLocaleDateString("pt-BR");
}