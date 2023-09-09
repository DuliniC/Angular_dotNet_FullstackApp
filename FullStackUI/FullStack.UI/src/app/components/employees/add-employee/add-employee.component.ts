import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';
import { EmployeeListComponent } from '../employee-list/employee-list.component';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})
export class AddEmployeeComponent {
  addEmployeeRequest: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    department: '',
  };

  constructor(
    private addEmployeesService: EmployeesService,
    private router: Router
  ) {}

  addEmployee() {
    this.addEmployeesService.addEmployees(this.addEmployeeRequest).subscribe({
      next: (employee) => {
        this.router.navigate(['employees']);
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
}
