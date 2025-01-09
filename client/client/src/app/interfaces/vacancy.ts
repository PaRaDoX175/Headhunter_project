export interface ICompany {
  id: number;
  name: string;
  industry: string;
  foundedYear: string;
  description: string;
  vacancies: IVacancy[];
}

export interface IVacancy {
  id: number;
  position: string;
  salary: number;
  department: string;
  location: string;
  requirements: string;
  responsibilities: string;
  email: string;
  description: string;
  companyName: string;
  companyId: number;
}
