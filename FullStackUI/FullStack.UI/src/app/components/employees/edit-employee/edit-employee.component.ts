import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  employeeDeatils: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    department: '',
  };

  constructor(private route: ActivatedRoute,
              private employeesService: EmployeesService,
              private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          this.employeesService.getEmployee(id)
          .subscribe({
            next: (response) => {
              this.employeeDeatils = response
            }
          })
        }
      }
    })
  }

  editEmployee(){
    this.employeesService.updateEmployee(this.employeeDeatils.id, this.employeeDeatils)
    .subscribe({
      next: (response) => {
        this.router.navigate(['employees']);
      },
      error: (response) => {
        console.log(response);
      },
    })
  }

  deleteEmployee(id: string){
    this.employeesService.deleteEmployee(id)
    .subscribe({
      next: (value) => {     
        this.router.navigate(['employees']);
      },
      error: (response) => {
        console.log(response);
      }
    })
  }
}
