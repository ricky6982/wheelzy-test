CREATE TABLE car
(
    id       INT PRIMARY KEY AUTO_INCREMENT,
    car_year INT,
    make     VARCHAR(255),
    model    VARCHAR(255),
    submodel VARCHAR(255)
);

CREATE TABLE Location
(
    id  INT PRIMARY KEY AUTO_INCREMENT,
    zip VARCHAR(255),
);

CREATE TABLE user
(
    id          INT PRIMARY KEY AUTO_INCREMENT,
    first_name  VARCHAR(255),
    last_name   VARCHAR(255),
    email       VARCHAR(255),
    password    VARCHAR(255),
    location_id INT,
    FOREIGN KEY (location_id) REFERENCES Location (id)
);

CREATE TABLE car_user
(
    id         INT PRIMARY KEY AUTO_INCREMENT,
    car_id     INT,
    user_id    INT,
    status, -- It'll be parsed as a Enum property in the Entity
    amount     DECIMAL(10, 2),
    current    bit default 0 not null,
    created_at datetime null,
    created_by INT FOREIGN KEY (user_id) REFERENCES user (id),
    FOREIGN KEY (car_id) REFERENCES car (id),
    FOREIGN KEY (user_id) REFERENCES user (id)
);

-- Status Enum: 'Pending Acceptance' = 0, 'Accepted' = 1, 'Picked Up' = 2
SELECT c.car_year, c.make, c.model, c.submodel, u.first_name, u.last_name, cu.amount, cu.status, cu.created_at
FROM car_user cu
         inner join car c on car_user.car_id = c.id
         inner join user u on car_user.user_id = u.id
WHERE cu.current = 1;



